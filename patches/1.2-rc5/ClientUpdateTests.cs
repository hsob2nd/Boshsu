using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StableRemastered.Updates;
class ClientUpdateTests
{
    static int checks;static string root;
    static void Check(bool pass,string label){checks++;if(!pass)throw new Exception(label);}
    static void Reject(Action action,string label){try{action();}catch{checks++;return;}throw new Exception(label+" accepted");}
    static string Make(string name,string tag="v1.2.0-rc.3",string extra=null,bool duplicate=false)
    {
        string file=Path.Combine(root,name+".zip"),temp=Path.Combine(root,name+"-files");Directory.CreateDirectory(temp);
        var content=new Dictionary<string,string>{{"Boshsu.exe","new application"},{"StableRemasteredGameplay.dll","new engine"},{"Boshsu.Updater.exe","new updater"},{"README.txt","new readme"},{"BOSHSU_BUILD.json",JsonConvert.SerializeObject(new{Product="Boshsu",ReleaseTag=tag})},{"materials/skin.png","fixture texture"}};
        if(extra!=null)content[extra]="fixture unexpected entry";
        var hashes=new Dictionary<string,string>();foreach(var pair in content){string p=Path.Combine(temp,Guid.NewGuid().ToString("N"));File.WriteAllText(p,pair.Value);hashes[pair.Key]=ClientUpdatePackage.Hash(p);}
        using(var z=ZipFile.Open(file,ZipArchiveMode.Create)){
            foreach(var pair in content){var e=z.CreateEntry(pair.Key);using(var w=new StreamWriter(e.Open()))w.Write(pair.Value);}
            using(var w=new StreamWriter(z.CreateEntry("BOSHSU_FILES.json").Open()))w.Write(JsonConvert.SerializeObject(hashes));
            if(duplicate)using(var w=new StreamWriter(z.CreateEntry("BOSHSU.EXE").Open()))w.Write("duplicate");
        }return file;
    }
    static JObject Release(string tag,bool beta=true,string url=null,string digest=null)=>JObject.FromObject(new{tag_name=tag,prerelease=beta,draft=false,assets=new[]{new{name="Boshsu-1.2-clean.zip",size=100,browser_download_url=url??"https://github.com/hsob2nd/Boshsu/releases/download/"+tag+"/Boshsu-1.2-clean.zip",digest=digest??"sha256:"+new string('a',64)}}});
    static string Selection(JArray releases,bool beta)
    {
        var type=typeof(ClientRelease).Assembly.GetType("StableRemastered.Application.Updates.GithubClientUpdates",true);
        var method=type.GetMethod("SelectRelease",BindingFlags.NonPublic|BindingFlags.Static);object result=method.Invoke(null,new object[]{releases,beta});return result.GetType().GetField("Tag").GetValue(result) as string;
    }
    static void Main(string[] args)
    {
        root=Path.GetFullPath(args[0]);Directory.CreateDirectory(root);
        Check(ClientRelease.Compare("v1.2.0-rc.10","v1.2.0-rc.2")>0,"Numeric RC ordering");Check(ClientRelease.Compare("v1.2.0","v1.2.0-rc.99")>0,"Stable supersedes beta");Check(ClientRelease.Compare("v1.3.0-alpha.1","v1.2.0")>0,"Next minor ordering");Reject(()=>ClientRelease.Compare("v1.2.0/evil","v1.2.0"),"Unsafe version");
        foreach(string n in new[]{"../Boshsu.exe","C:/Boshsu.exe","materials/../../scores.db","materials/CON.dll","materials/file:stream","materials/file.","Logs/runtime.log","StableRemasteredAccounts/profiles.json","osu!.baile.cfg","scores.db","BoshShell.json","MapLab/song.osu","Skins/cursor.png","PPProviders/custom.json"})Check(!ClientUpdatePackage.AllowedEntry(n),"Protected/unsafe path "+n);
        Check(ClientUpdatePackage.AllowedEntry("materials/skin.png")&&ClientUpdatePackage.AllowedEntry("fonts/Bosh.ttf")&&ClientUpdatePackage.AllowedEntry("MaxAssistTelemetry.paused")&&ClientUpdatePackage.AllowedEntry("RELEASE_STATUS.txt"),"Shipped resources accepted");
        Check(Selection(new JArray(Release("v1.2.0-rc.10"),Release("v1.2.0-rc.3")),true)=="v1.2.0-rc.10","Newest beta selected");
        Check(Selection(new JArray(Release("v1.2.0-rc.10"),Release("v1.2.0",false)),false)=="v1.2.0","Stable channel excludes beta");
        Check(Selection(new JArray(Release("v1.2.0-rc.1")),true)==null,"No downgrade");
        Check(Selection(new JArray(Release("v1.2.0-rc.3",true,"https://evil.example/update.zip")),true)==null,"Untrusted asset refused");
        Check(Selection(new JArray(Release("v1.2.0-rc.3",true,null,"missing")),true)==null,"Missing GitHub digest refused");
        string good=Make("good");Check(ClientUpdatePackage.Verify(good,ClientUpdatePackage.Hash(good),"v1.2.0-rc.3").Count==6,"Complete manifest verified");
        Reject(()=>ClientUpdatePackage.Verify(good,new string('0',64),"v1.2.0-rc.3"),"Wrong package digest");Reject(()=>ClientUpdatePackage.Verify(good,ClientUpdatePackage.Hash(good),"v1.2.0-rc.4"),"Wrong release identity");
        foreach(string path in new[]{"../escape.dll","StableRemasteredAccounts/profiles.json","osu!.baile.cfg"}){string zip=Make("bad-"+checks,"v1.2.0-rc.3",path);Reject(()=>ClientUpdatePackage.Verify(zip,ClientUpdatePackage.Hash(zip),"v1.2.0-rc.3"),"Unsafe archive "+path);}
        string dup=Make("duplicate","v1.2.0-rc.3",null,true);Reject(()=>ClientUpdatePackage.Verify(dup,ClientUpdatePackage.Hash(dup),"v1.2.0-rc.3"),"Case duplicate archive");
        string install=Path.Combine(root,"installation");Directory.CreateDirectory(install);File.WriteAllText(Path.Combine(install,"Boshsu.exe"),"old application");File.WriteAllText(Path.Combine(install,"BOSHSU_BUILD.json"),"old metadata");
        var personal=new Dictionary<string,string>{{"osu!.baile.cfg","display settings"},{"BoshShell.json","cosmetics"},{"V02Balance.json","personal rewards"},{"StableRemasteredAccounts/profiles.json","fixture identity"},{"StableRemasteredAccounts/scores/score.json","fixture score"},{"replays/play.ureplay","fixture replay"},{"Songs/map.osu","fixture map"},{"Skins/cursor.png","fixture skin"},{"PPProviders/custom.json","custom PP"}};
        foreach(var p in personal){string f=Path.Combine(install,p.Key);Directory.CreateDirectory(Path.GetDirectoryName(f));File.WriteAllText(f,p.Value);}
        var job=new UpdateJob{Root=install,Archive=good,SHA256=ClientUpdatePackage.Hash(good),Tag="v1.2.0-rc.3"};
        Reject(()=>ClientUpdatePackage.Install(job,n=>{if(n==2)throw new IOException("Injected write failure");}),"Install failure");Check(File.ReadAllText(Path.Combine(install,"Boshsu.exe"))=="old application"&&File.ReadAllText(Path.Combine(install,"BOSHSU_BUILD.json"))=="old metadata","Failed install rolled back previous files");
        foreach(var p in personal)Check(File.ReadAllText(Path.Combine(install,p.Key))==p.Value,"Failure preserved "+p.Key);
        ClientUpdatePackage.Install(job);Check(File.ReadAllText(Path.Combine(install,"Boshsu.exe"))=="new application","Application updated");
        foreach(var p in personal)Check(File.ReadAllText(Path.Combine(install,p.Key))==p.Value,"Update preserved "+p.Key);
        Check(Directory.GetDirectories(Path.Combine(install,"Updates"),"rollback-*").Length==1,"One recovery version retained");ClientUpdatePackage.Install(job);Check(Directory.GetDirectories(Path.Combine(install,"Updates"),"rollback-*").Length==1,"Recovery storage remains bounded");Check(Directory.GetDirectories(Path.Combine(install,"Updates"),"stage-*").Length==0,"Staging resources released");
        string checksum=Path.Combine(root,"SHA256SUMS.txt"),hash=ClientUpdatePackage.Hash(good);
        File.WriteAllText(checksum,hash+"  "+Path.GetFileName(good)+"\n");
        var manual=ClientUpdatePackage.PrepareManual(install,good,checksum);
        Check(manual.Root==install&&manual.Archive==good&&manual.SHA256==hash&&manual.Tag=="v1.2.0-rc.3","Manual ZIP selects the same complete package and target");
        Check(ClientUpdatePackage.ReadChecksum(hash.ToUpperInvariant()+" *"+Path.GetFileName(good)+"\r\n",Path.GetFileName(good))==hash,"Binary checksums / uppercase / CRLF supported");
        Reject(()=>ClientUpdatePackage.ReadChecksum(hash+"  other.zip\n",Path.GetFileName(good)),"Wrong checksum filename");
        Reject(()=>ClientUpdatePackage.ReadChecksum(hash+"  good.zip\n"+hash+"  good.zip\n","good.zip"),"Duplicate checksum entries");
        Reject(()=>ClientUpdatePackage.ReadChecksum(new string('a',65537),"good.zip"),"Oversized checksum input");
        Reject(()=>ClientUpdatePackage.ReadChecksum("missing","good.zip"),"Missing checksum input");
        File.WriteAllText(checksum,new string('0',64)+"  good.zip\n");Reject(()=>ClientUpdatePackage.PrepareManual(install,good,checksum),"Manual corrupt package digest");
        File.WriteAllText(checksum,hash+"  good.zip\n");
        File.WriteAllText(Path.Combine(install,"BOSHSU_BUILD.json"),JsonConvert.SerializeObject(new{Product="Boshsu",ReleaseTag="v1.2.0-rc.2"}));
        Check(ClientUpdatePackage.PrepareManual(install,good,checksum).Tag=="v1.2.0-rc.3","Manual upgrade accepted");
        File.WriteAllText(Path.Combine(install,"BOSHSU_BUILD.json"),JsonConvert.SerializeObject(new{Product="Boshsu",ReleaseTag="v1.2.0-rc.4"}));
        Reject(()=>ClientUpdatePackage.PrepareManual(install,good,checksum),"Manual downgrade rejected");
        File.WriteAllText(Path.Combine(install,"BOSHSU_BUILD.json"),JsonConvert.SerializeObject(new{Product="Other",ReleaseTag="v1.2.0-rc.2"}));
        Reject(()=>ClientUpdatePackage.PrepareManual(install,good,checksum),"Foreign installation rejected");
        File.WriteAllText(Path.Combine(install,"BOSHSU_BUILD.json"),JsonConvert.SerializeObject(new{Product="Boshsu",ReleaseTag="v1.2.0-rc.2"}));
        string source=Path.Combine(root,"source.zip");using(var z=ZipFile.Open(source,ZipArchiveMode.Create))using(var w=new StreamWriter(z.CreateEntry("source.cs").Open()))w.Write("source");
        Reject(()=>ClientUpdatePackage.PrepareManual(install,source,checksum),"Source ZIP cannot be installed");
        Reject(()=>ClientUpdatePackage.PrepareManual(root,good,checksum),"Non-installation target rejected");
        string helper=Path.Combine(root,"helper");Directory.CreateDirectory(helper);File.WriteAllText(Path.Combine(helper,"Boshsu.Updater.exe"),"helper fixture");File.WriteAllText(Path.Combine(helper,"Newtonsoft.Json.dll"),"dependency fixture");
        string staged=ClientUpdatePackage.StageManual(manual,helper,123);
        var stagedJob=JsonConvert.DeserializeObject<UpdateJob>(File.ReadAllText(Path.Combine(staged,"job.json")));
        Check(stagedJob.ParentId==123&&stagedJob.SHA256==hash&&stagedJob.Root==install,"Manual helper waits for its launching updater");
        Check(File.ReadAllText(Path.Combine(staged,"Boshsu.Updater.exe"))=="helper fixture"&&staged!=helper,"Updater runs outside the files it replaces");
        string second=ClientUpdatePackage.StageManual(manual,helper,124);
        Check(second!=staged&&!Directory.Exists(staged)&&Directory.GetDirectories(Path.Combine(install,"Updates"),"manual-*").Length==1,"Completed manual helper storage bounded");
        string unowned=Path.Combine(install,"Updates","manual-player-data");Directory.CreateDirectory(unowned);File.WriteAllText(Path.Combine(unowned,"mine.txt"),"keep");
        ClientUpdatePackage.StageManual(manual,helper,125);Check(File.ReadAllText(Path.Combine(unowned,"mine.txt"))=="keep","Unowned update folders retained");
        Reject(()=>ClientUpdatePackage.StageManual(manual,root,126),"Missing updater helper refused");
        Check(Directory.GetDirectories(Path.Combine(install,"Updates"),"manual-*").Length==2,"Failed helper preparation cleaned up");
        ClientUpdatePackage.Install(manual);foreach(var p in personal)Check(File.ReadAllText(Path.Combine(install,p.Key))==p.Value,"Manual update preserved "+p.Key);
        File.WriteAllText(Path.Combine(root,"UPDATER_TESTS.json"),JsonConvert.SerializeObject(new{Passed=true,Checks=checks,PlayerDataUsed=false,NetworkUsed=false,Tests="Release selection, digest/manifest/identity/path validation, protected data, transactional rollback, bounded recovery"},Formatting.Indented));Console.WriteLine(checks+" updater checks passed");
    }
}
