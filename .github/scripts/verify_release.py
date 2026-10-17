"""Read-only GitHub release/package integrity check. Does not execute downloads."""
import hashlib,io,json,os,re,urllib.request,zipfile

REPO='hsob2nd/Boshsu'
TAG=os.environ['RELEASE_TAG']
if not re.fullmatch(r'v\d+\.\d+\.\d+(?:-(?:alpha|beta|rc)\.\d+)?',TAG):raise ValueError('Invalid release tag')

def download(url,limit):
    request=urllib.request.Request(url,headers={'User-Agent':'Boshsu-release-integrity','Accept':'application/vnd.github+json'})
    with urllib.request.urlopen(request,timeout=120) as response:
        value=response.read(limit+1)
    if len(value)>limit:raise ValueError('Download size limit exceeded')
    return value

release=json.loads(download(f'https://api.github.com/repos/{REPO}/releases/tags/{TAG}',2*1024*1024))
if release['draft'] or release['tag_name']!=TAG:raise ValueError('Release identity is unavailable')
assets={a['name']:a for a in release['assets']}
sums=assets['SHA256SUMS.txt'];sum_url=f'https://github.com/{REPO}/releases/download/{TAG}/SHA256SUMS.txt'
if sums['browser_download_url']!=sum_url:raise ValueError('Checksum source differs')
checksums={}
for line in download(sum_url,65536).decode('utf-8-sig').splitlines():
    match=re.fullmatch(r'([0-9a-f]{64})  (Boshsu-\d+(?:\.\d+){1,3}-(?:clean|source)\.zip)',line)
    if not match or match[2] in checksums:raise ValueError('Invalid or duplicate checksum entry')
    checksums[match[2]]=match[1]
if len(checksums)!=2:raise ValueError('Require exactly one clean and one source package')
reports=[]
for name,expected in checksums.items():
    asset=assets[name];url=f'https://github.com/{REPO}/releases/download/{TAG}/{name}'
    if asset['browser_download_url']!=url or asset.get('digest')!='sha256:'+expected:raise ValueError('GitHub digest/origin differs')
    data=download(url,512*1024*1024)
    if len(data)!=asset['size'] or hashlib.sha256(data).hexdigest()!=expected:raise ValueError('Package checksum differs')
    with zipfile.ZipFile(io.BytesIO(data)) as archive:
        entries=archive.infolist();names=[e.filename for e in entries]
        if len(names)>50000 or len(set(n.casefold() for n in names))!=len(names):raise ValueError('Duplicate or excessive entries')
        if sum(e.file_size for e in entries)>2*1024**3:raise ValueError('Expanded package too large')
        for entry in entries:
            parts=entry.filename.replace('\\','/').split('/')
            if entry.filename.startswith(('/','\\')) or any(p in ('..','.git','checkpoints','evidence') for p in parts) or ':' in entry.filename:raise ValueError('Private/unsafe package path')
        def digest(member):
            h=hashlib.sha256()
            with archive.open(member) as f:
                for chunk in iter(lambda:f.read(1024*1024),b''):h.update(chunk)
            return h.hexdigest()
        if '-clean.' in name:
            build=json.loads(archive.read('BOSHSU_BUILD.json'));manifest=json.loads(archive.read('BOSHSU_FILES.json'))
            if build['Product']!='Boshsu' or build['ReleaseTag']!=TAG or build.get('PersonalDataIncluded')!=False:raise ValueError('Runtime identity/privacy metadata differs')
            for filename in names:
                if filename.split('/')[0].casefold() in {'stableremasteredaccounts','songs','skins','replays','logs','performance','maplab','updates','maxassisttelemetry'} or filename.casefold().endswith(('.cfg','.jsonl','.log')):raise ValueError('User/runtime data in clean package')
                if filename.lower().startswith('ppproviders/') and filename not in {'PPProviders/akatsuki-b96019f.zip','PPProviders/ccv3-pp-main.zip'}:raise ValueError('Custom provider data in clean package')
            if set(manifest)|{'BOSHSU_FILES.json'}!=set(names):raise ValueError('Runtime manifest coverage differs')
            for member,sha in manifest.items():
                if digest(member)!=sha:raise ValueError('Runtime member differs: '+member)
            for member in ('Boshsu.exe','Boshsu.Updater.exe','StableRemasteredGameplay.dll','UltimatePersonalModel.dll','personal-golden-qualified.pgold'):
                if member not in manifest:raise ValueError('Incomplete portable runtime')
        else:
            pin_file='stable-remastered/engine/pins/McEngine.sha256'
            for line in archive.read(pin_file).decode().splitlines():
                sha,relative=line.split('  ',1)
                if digest('build/ultimate-migration/upstream/McEngine/McEngine/'+relative)!=sha:raise ValueError('Corresponding pinned source missing or altered')
            for member in ('stable-remastered/CMakeLists.txt','stable-remastered/StableRemastered.sln','stable-remastered/engine/LICENSE','stable-remastered/tools/Build.ps1'):
                if member not in names:raise ValueError('Incomplete corresponding source')
        reports.append(dict(Name=name,Files=len(names),SHA256=expected,Bytes=len(data)))
print(json.dumps(dict(Passed=True,Tag=TAG,Packages=reports,DownloadedSoftwareExecuted=False),indent=2))
