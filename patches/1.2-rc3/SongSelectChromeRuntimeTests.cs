// External fixture: real client rendering/lifecycle in a disposable package.
// No production test hook, personal data, or OS input injection is used here.
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json;
using StableRemastered.Shell;
class SongSelectChromeRuntimeTests
{
    const BindingFlags F=BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.Instance;
    static readonly Assembly Client=typeof(ShellLayout).Assembly;
    static readonly List<object> Records=new List<object>();
    static readonly Stopwatch Clock=Stopwatch.StartNew();
    static readonly string[] Skins={"Default","Rafis 2017-08-10","-  Snowy DT (blue cursor)","Chrome Classic Tall","Chrome Modern Tall"};
    static readonly int[] Widths={1280,1024,1280,1600,1920},Heights={720,768,800,900,1080};
    static readonly double[] Scales={0,.75,1,1.25,0};
    static readonly bool Baseline=File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"baseline.txt"));
    static Timer timer;static int phase,cycle,checks;static double due;static bool done;
    sealed class StartTimerFilter:IMessageFilter { bool started;public bool PreFilterMessage(ref Message m){if(!started){started=true;timer.Start();}return false;} }
    static Type T(string n)=>Client.GetType(n,true);
    static object Get(Type t,string n,object o=null)=>t.GetProperty(n,F)?.GetValue(o)??t.GetField(n,F)?.GetValue(o);
    static object Get(object o,string n)=>Get(o.GetType(),n,o);
    static void Set(Type t,string n,object v,object o=null){var p=t.GetProperty(n,F);if(p!=null)p.SetValue(o,v);else t.GetField(n,F).SetValue(o,v);}
    static object Call(Type t,string n,params object[] a)=>t.GetMethods(F).Single(m=>m.Name==n&&m.GetParameters().Length==a.Length).Invoke(null,a);
    static Type G=>T("osu.GameBase");static Form Form=>(Form)Get(G,"Form");
    static void Mode(string n)=>Call(G,"ChangeMode",Enum.Parse(T("osu.OsuModes"),n),false);
    static void Config(string n,object v){var b=Get(T("osu.Configuration.ConfigManager"),n);Set(b.GetType(),"Value",v,b);}
    static void Wait(int p,double ms=300){phase=p;due=Clock.Elapsed.TotalMilliseconds+ms;}
    static void Check(bool ok,string label){checks++;if(!ok)throw new Exception(label);}
    static IEnumerable<object> Sprites(object manager)=>((IEnumerable)Get(manager,"SpriteList")).Cast<object>();
    static bool Hidden(object s)=>(bool)Get(s,"Bypass")&&!(bool)Get(s,"HandleInput");
    static string Asset(object s){var tex=Get(s,"Texture");return tex==null?null:Get(tex,"AssetName") as string;}
    static string Name(object s)=>(Asset(s)??s.GetType().Name);
    static void Inspect()
    {
        var selection=Get(G,"RunningGameMode");var high=Sprites(Get(selection,"spriteManagerHigh")).ToArray();
        var oldButtons=high.Where(s=>new[]{"selection-mode","selection-mods","selection-random","selection-options"}.Any(n=>Name(s).Contains(n))).ToArray();
        var leaked=oldButtons.Where(s=>!Hidden(s)).Select(Name).ToArray();
        Check(oldButtons.Length==8,"Both normal and hover variants exist for all four legacy controls");
        if(Baseline)Check(leaked.Length==4,"Negative control reproduces all four visible normal buttons");
        else {
            Check(leaked.Length==0,"No legacy button draws or captures input");
            var chrome=((IEnumerable)Get(selection,"legacyShellChrome")).Cast<object>().ToArray();
            Check(chrome.Length>=15,"Explicit shell chrome includes both header backings, footer, cookie and mod summary");
            Check(chrome.All(Hidden),"Every replaced chrome instance is bypassed with input disabled");
            foreach(string n in new[]{"details","details2","details3","details4","details5","details6","searchText","searchInfo","speedLabel","diffCalcText","boshGroupLabel","boshSortLabel"})Check(Hidden(Get(selection,n)),"Old header hidden: "+n);
            foreach(var s in ((IEnumerable)Get(Get(selection,"backButton"),"SpriteCollection")).Cast<object>())Check(Hidden(s),"Legacy Back artwork and input hidden");
            var modern=Get(selection,"boshShell");Check(modern!=null,"Modern Song Select owns the replacement controls");
            var shellSprites=Sprites(Get(modern,"manager")).ToArray();
            Check(shellSprites.Any(s=>(bool)Get(s,"HandleInput")&&!(bool)Get(s,"Bypass")),"Modern footer retains interactive controls");
            if(cycle/5>0){var icon=Call(T("osu.Graphics.TextureManager"),"Load","selection-mods",Enum.Parse(T("osu.Graphics.Skinning.SkinSource"),"Skin"),null);if(icon!=null)Check(shellSprites.Any(s=>Name(s).Contains("selection-mods")&&!(bool)Get(s,"Bypass")),"Available shared skin icon remains visible in the modern footer");}
            Check(Sprites(Get(selection,"spriteManagerBackground")).Any(s=>!(bool)Get(s,"Bypass")),"Beatmap background remains drawn");
            // Old lifecycle updates can animate alpha without reviving bypassed art.
            Set(Get(selection,"buttonMods").GetType(),"Alpha",1f,Get(selection,"buttonMods"));
            Check(Hidden(Get(selection,"buttonMods")),"Legacy hover animation cannot revive the old input owner");
        }
        var wm=Get(G,"WindowManager");
        Records.Add(new{Cycle=cycle,Skin=Skins[cycle/5],Width=Get(wm,"Width"),Height=Get(wm,"Height"),UIScale=Scales[cycle%5],LegacyButtons=oldButtons.Select(Name).ToArray(),VisibleLegacyButtons=leaked});
        File.WriteAllText("chrome-current.json",JsonConvert.SerializeObject(Records.Last(),Formatting.Indented));
    }
    static void Tick(object sender,EventArgs e)
    {
        if(done||Clock.Elapsed.TotalMilliseconds<due)return;
        try {
            if(Clock.Elapsed.TotalMinutes>15)throw new TimeoutException("Chrome fixture phase "+phase);
            switch(phase){
            case 0:
                if(Get(G,"Mode").ToString()!="Menu"||Get(T("osu.GameplayElements.Beatmaps.BeatmapManager"),"Beatmaps")==null||!File.Exists("chrome-ready.txt"))return;
                Wait(1);break;
            case 1:
                int i=cycle%5;Config("sFullscreen",i==4);Config("sLetterboxing",false);Config("sRawInput",i%2==1);Config("sAbsoluteToOsuWindow",false);
                var preferences=T("StableRemastered.Application.Shell.ShellPreferences");var settings=(ShellSettings)Get(preferences,"Current");settings.UIScale=Scales[i];Call(preferences,"Save",settings);
                Call(G,"SetScreenSize",Widths[i],Heights[i],true,true);
                Config("sSkin",Skins[cycle/5]);Call(T("osu.Graphics.Skinning.SkinManager"),"LoadSkin",Skins[cycle/5],true);
                Mode("SelectPlay");Wait(2,1200);break;
            case 2:
                if(Get(G,"Mode").ToString()!="SelectPlay"||Get(Get(G,"RunningGameMode"),"boshShell")==null)return;
                Inspect();Wait(3);break;
            case 3:
                // Snapshot holds are controlled by the observer, never by timed input.
                if((Baseline||cycle<5||cycle%5==0)&&!File.Exists("chrome-continue-"+cycle+".txt"))return;
                if(Baseline||++cycle==Skins.Length*5){Finish(null);return;}
                Mode("Menu");Wait(4,450);break;
            case 4:if(Get(G,"Mode").ToString()=="Menu")Wait(1);break;
            }
        }catch(Exception error){Finish(error.ToString());}
    }
    static void Finish(string error)
    {
        done=true;timer.Stop();Directory.CreateDirectory("chrome-evidence");File.WriteAllText("chrome-evidence/summary.json",JsonConvert.SerializeObject(new{Passed=error==null,Baseline,Checks=checks,Cycles=Records.Count,Error=error,Records},Formatting.Indented));Form?.Close();
    }
    [STAThread]static int Main()
    {
        if(!new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Name.StartsWith("chrome-qualification"))return 2;
        Environment.SetEnvironmentVariable("BOSH_TEST_MONITOR",@"\\.\DISPLAY2");
        timer=new Timer{Interval=40};timer.Tick+=Tick;Application.AddMessageFilter(new StartTimerFilter());
        try{Client.EntryPoint.Invoke(null,new object[]{new string[0]});}catch(Exception error){Finish(error.ToString());return 1;}return done?0:1;
    }
}
