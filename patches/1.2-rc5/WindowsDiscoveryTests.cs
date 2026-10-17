using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Newtonsoft.Json;
using StableRemastered.Application;
using StableRemastered.Shell;

class WindowsDiscoveryTests
{
    static int checks;static string root;
    [DllImport("shell32.dll")]static extern int GetCurrentProcessExplicitAppUserModelID(out IntPtr value);
    static void Check(bool condition,string message){if(!condition)throw new Exception(message);checks++;}
    static string Value(RegistryKey user,string key,string name=""){using(var k=user.OpenSubKey(key))return k?.GetValue(name) as string;}
    static bool Exists(RegistryKey user,string key){using(var k=user.OpenSubKey(key))return k!=null;}
    static void ReadLink(string path,out string target,out string id)
    {
        var args=new object[]{path,null,null};
        bool exists=(bool)typeof(WindowsApplicationIdentity).GetMethod("ReadShortcut",BindingFlags.NonPublic|BindingFlags.Static).Invoke(null,args);
        Check(exists,"Shell shortcut can be loaded after save");target=(string)args[1];id=(string)args[2];
    }
    [STAThread]static int Main(string[] args)
    {
        if(args.Length!=1)return 2;string evidence=Path.GetFullPath(args[0]);Directory.CreateDirectory(evidence);
        root=Path.Combine(evidence,"run-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(root);
        string key=@"Software\Boshsu\DiscoveryTests\"+Guid.NewGuid().ToString("N");
        try{
            WindowsApplicationIdentity.InitializeProcess();IntPtr id;
            Marshal.ThrowExceptionForHR(GetCurrentProcessExplicitAppUserModelID(out id));
            try{Check(Marshal.PtrToStringUni(id)==WindowsApplicationIdentity.AppId,"Process uses the documented stable AppUserModelID");}finally{Marshal.FreeCoTaskMem(id);}
            var historical=JsonConvert.DeserializeObject<ShellSettings>("{\"SchemaVersion\":1}");
            Check(historical.RegisterWithWindows,"Old preferences receive the registration default");
            historical.RegisterWithWindows=false;Check(!JsonConvert.DeserializeObject<ShellSettings>(JsonConvert.SerializeObject(historical)).RegisterWithWindows,"Opt out persists independently of performance profiles");
            foreach(PerformanceProfile p in Enum.GetValues(typeof(PerformanceProfile))){historical.Apply(p,8);Check(!historical.RegisterWithWindows,"Performance profile cannot re-enable registration: "+p);}
            using(var user=Registry.CurrentUser.CreateSubKey(key)){
                user.SetValue("Sentinel","untouched");
                string a=Path.Combine(root,"Copy A — unicode","Boshsu.exe"),b=Path.Combine(root,"Copy B","Boshsu.exe"),link=Path.Combine(root,"Start Menu","Boshsu.lnk");
                Directory.CreateDirectory(Path.GetDirectoryName(a));Directory.CreateDirectory(Path.GetDirectoryName(b));File.WriteAllText(a,"unexecuted fixture");File.WriteAllText(b,"unexecuted fixture");
                WindowsApplicationIdentity.Apply(user,a,link,false);
                Check(!Exists(user,WindowsApplicationIdentity.AppPathKey)&&!File.Exists(link),"Disabled registration creates no keys or shortcut");
                WindowsApplicationIdentity.Apply(user,a,link,true);
                Check(Value(user,WindowsApplicationIdentity.AppPathKey)==a,"App Paths resolves the exact Unicode executable path");
                Check(Value(user,WindowsApplicationIdentity.ApplicationKey,"FriendlyAppName")=="Boshsu","Friendly application name is registered");
                Check(Value(user,WindowsApplicationIdentity.ApplicationKey+@"\shell\open\command")=="\""+a+"\"","Launch command correctly quotes spaces and Unicode");
                Check(Value(user,WindowsApplicationIdentity.ApplicationKey+@"\DefaultIcon")=="\""+a+"\",0","Application icon uses its executable");
                Check(Value(user,WindowsApplicationIdentity.AppPathKey,"Path")==null,"Registration does not change process/global PATH");
                string target,linkId;ReadLink(link,out target,out linkId);
                Check(target==a && linkId==WindowsApplicationIdentity.AppId,"Shortcut target and AppUserModelID agree with the process");
                var stamp=File.GetLastWriteTimeUtc(link);var bytes=File.ReadAllBytes(link);
                WindowsApplicationIdentity.Apply(user,a,link,true);
                Check(File.GetLastWriteTimeUtc(link)==stamp && Convert.ToBase64String(File.ReadAllBytes(link))==Convert.ToBase64String(bytes),"Repeated startup does not rewrite the shortcut");
                WindowsApplicationIdentity.Apply(user,b,link,true);ReadLink(link,out target,out linkId);
                Check(Value(user,WindowsApplicationIdentity.AppPathKey)==b && target==b,"Registration follows the most recently registered copy");
                WindowsApplicationIdentity.Apply(user,a,link,false);
                Check(Value(user,WindowsApplicationIdentity.AppPathKey)==b && File.Exists(link),"Old copy cannot remove the newer copy's registration");
                WindowsApplicationIdentity.Apply(user,b,link,false);
                Check(!Exists(user,WindowsApplicationIdentity.AppPathKey)&&!Exists(user,WindowsApplicationIdentity.ApplicationKey)&&!File.Exists(link),"Opt out removes only this copy's owned entries");
                Check((string)user.GetValue("Sentinel")=="untouched","Unrelated registry values survive cleanup");
                using(var foreign=user.CreateSubKey(WindowsApplicationIdentity.AppPathKey))foreign.SetValue("","foreign installer");
                bool refused=false;try{WindowsApplicationIdentity.Apply(user,a,link,true);}catch(IOException){refused=true;}
                Check(refused && Value(user,WindowsApplicationIdentity.AppPathKey)=="foreign installer" && !File.Exists(link),"A foreign registration is preserved without partial shortcut creation");
                WindowsApplicationIdentity.Apply(user,a,link,false);
                Check(Value(user,WindowsApplicationIdentity.AppPathKey)=="foreign installer","Disabling cannot remove a foreign registration");
                user.DeleteSubKeyTree(WindowsApplicationIdentity.AppPathKey);
                Directory.CreateDirectory(Path.GetDirectoryName(link));File.WriteAllText(link,"foreign/unreadable shortcut");
                refused=false;try{WindowsApplicationIdentity.Apply(user,a,link,true);}catch(Exception e) when(e is IOException || e is COMException){refused=true;}
                Check(refused && File.ReadAllText(link)=="foreign/unreadable shortcut" && !Exists(user,WindowsApplicationIdentity.AppPathKey),"Unreadable shortcuts are retained and fail before registry changes");
                File.Delete(link);WindowsApplicationIdentity.Apply(user,a,link,true);File.WriteAllText(link,"damaged owned shortcut");
                try{WindowsApplicationIdentity.Apply(user,a,link,false);}catch(COMException){ }
                Check(!Exists(user,WindowsApplicationIdentity.AppPathKey)&&!Exists(user,WindowsApplicationIdentity.ApplicationKey)&&File.ReadAllText(link)=="damaged owned shortcut","A damaged shortcut cannot prevent registry opt out and is retained");
                foreach(string invalid in new[]{Path.Combine(root,"Other.exe"),Path.Combine(root,"missing","Boshsu.exe")}){
                    refused=false;try{WindowsApplicationIdentity.Apply(user,invalid,link,true);}catch(Exception e) when(e is ArgumentException || e is FileNotFoundException){refused=true;}
                    Check(refused,"Invalid or missing executable cannot be registered");
                }
            }
            WindowsApplicationIdentity.ApplyPreference(true);Check(WindowsApplicationIdentity.Status.Contains("skipped"),"Qualification host cannot register itself as the player's client");
            File.WriteAllText(Path.Combine(evidence,"summary.json"),JsonConvert.SerializeObject(new{Passed=true,Checks=checks,GameLaunched=false,RecorderLaunched=false,IsolatedRegistrySubtree=true,AutomaticRecorderRecognitionCertified=false},Formatting.Indented));
            Console.WriteLine(checks+" Windows discovery checks passed");return 0;
        }catch(Exception e){File.WriteAllText(Path.Combine(evidence,"summary.json"),JsonConvert.SerializeObject(new{Passed=false,Checks=checks,Error=e.ToString()},Formatting.Indented));Console.Error.WriteLine(e);return 1;}
        finally{Registry.CurrentUser.DeleteSubKeyTree(key,false);}
    }
}
