param([Parameter(Mandatory=$true)][ValidatePattern('^v\d+\.\d+\.\d+(?:-(?:alpha|beta|rc)\.\d+)?$')][string]$Tag)
$ErrorActionPreference='Stop'
# Only matching, checksum-verified public release inputs; no credentials or GUI.
$release=Invoke-RestMethod -Uri "https://api.github.com/repos/hsob2nd/Boshsu/releases/tags/$Tag"
if ($release.draft -or $release.tag_name -ne $Tag) { throw 'Published release identity differs' }
$root=Join-Path $env:RUNNER_TEMP ('boshsu-cpu-'+[Guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $root | Out-Null
$sums=Invoke-RestMethod -Uri "https://github.com/hsob2nd/Boshsu/releases/download/$Tag/SHA256SUMS.txt"
$packages=@{}
foreach($line in ($sums -split "`n")) {
    if (!$line.Trim()) {continue}
    if ($line.Trim() -notmatch '^([0-9a-f]{64})  (Boshsu-\d+(?:\.\d+){1,3}-(clean|source)\.zip)$') {throw 'Invalid checksum entry'}
    $expected=$Matches[1];$name=$Matches[2];$kind=$Matches[3]
    if ($packages.ContainsKey($kind)) {throw 'Duplicate package kind'}
    $asset=@($release.assets | Where-Object name -eq $name)
    $url="https://github.com/hsob2nd/Boshsu/releases/download/$Tag/$name"
    if ($asset.Count -ne 1 -or $asset[0].browser_download_url -ne $url -or $asset[0].digest -ne "sha256:$expected" -or $asset[0].size -gt 536870912) {throw 'Asset identity differs'}
    $zip=Join-Path $root $name
    Invoke-WebRequest -Uri $url -OutFile $zip
    if ((Get-FileHash -LiteralPath $zip -Algorithm SHA256).Hash.ToLowerInvariant() -ne $expected) {throw 'Package hash differs'}
    $target=Join-Path $root $kind
    Add-Type -AssemblyName System.IO.Compression.FileSystem
    $inspect=[System.IO.Compression.ZipFile]::OpenRead($zip)
    try {
        $seen=New-Object 'System.Collections.Generic.HashSet[string]' ([StringComparer]::OrdinalIgnoreCase)
        [long]$expanded=0
        foreach($entry in $inspect.Entries) {
            $member=$entry.FullName.Replace('\','/')
            if ($member.StartsWith('/') -or $member.Contains(':') -or ($member.Split('/') | Where-Object {$_ -in @('..','.git','checkpoints','evidence')}) -or !$seen.Add($member)) {throw 'Unsafe or duplicate archive member'}
            $expanded+=$entry.Length
        }
        if ($seen.Count -gt 50000 -or $expanded -gt 2147483648) {throw 'Expanded archive exceeds bounds'}
    } finally {$inspect.Dispose()}
    [System.IO.Compression.ZipFile]::ExtractToDirectory($zip,$target)
    $packages[$kind]=$target
}
if ($packages.Count -ne 2) {throw 'Both matching packages are required'}
$source=Join-Path $packages.source 'stable-remastered'
& (Join-Path $source 'tools/Test-Release.ps1') -Runtime $packages.clean -Evidence (Join-Path $root 'release-tests')
& python (Join-Path $source 'tools/restore_runtime_dependencies.py') --runtime $packages.clean
if ($LASTEXITCODE -ne 0) {throw 'Pinned test dependencies could not be restored'}
$hostFolder=Join-Path $source 'frontend/Bosh/osu!win/bin/x64/Release'
New-Item -ItemType Directory -Path $hostFolder -Force | Out-Null
Get-ChildItem -LiteralPath $packages.clean -File -Filter '*.dll' | Copy-Item -Destination $hostFolder
& (Join-Path $source 'tools/Test-Accounts.ps1')
Write-Output 'Portable-host contracts/navigation/update and source-compiled account/PP-provider fixtures passed. This is not an OpenGL, physical-input or complete source-build certificate.'
