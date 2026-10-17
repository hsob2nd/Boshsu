// Runs the real client/engine in a disposable package. Injects raw packets at
// the production application boundary; this is not a physical-device claim.
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using StableRemastered.Gameplay;
using StableRemastered.Accounts;
class RawInputRuntimeTests
{
    const BindingFlags F=BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.Instance;
    static readonly Assembly Client=typeof(GameplaySettings).Assembly;
    static readonly List<object> Records=new List<object>();
    static readonly Stopwatch Clock=Stopwatch.StartNew();
    static Timer timer;static int phase,cycle,checks;static double due;static bool done;
    static readonly bool UpdateOnly=File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"update-ui-only.txt"));
    sealed class StartTimerFilter : IMessageFilter
    {
        bool started;
        public bool PreFilterMessage(ref Message m){if(!started){started=true;timer.Start();}return false;}
    }
    static IGameplayEngine engine;static StableGameplayContext context;static double[] before;static double expectedX,expectedY;static ulong epoch;static DisplayState testDisplay;
    [DllImport("StableRemasteredGameplay.dll",CallingConvention=CallingConvention.Cdecl)]static extern int sr_engine_pointer_state_v1([Out]double[] values,uint count);
    static Type T(string n)=>Client.GetType(n,true);
    static object Get(Type t,string n,object owner=null)=>t.GetProperty(n,F)?.GetValue(owner)??t.GetField(n,F)?.GetValue(owner);
    static void Set(Type t,string n,object value,object owner=null){var p=t.GetProperty(n,F);if(p!=null)p.SetValue(owner,value);else t.GetField(n,F).SetValue(owner,value);}
    static object Call(Type t,string n,params object[] args)=>t.GetMethods(F).Single(m=>m.Name==n&&m.GetParameters().Length==args.Length).Invoke(null,args);
    static Type G=>T("osu.GameBase");static Type Services=>T("StableRemastered.Application.ApplicationServices");
    static Form Form=>(Form)Get(G,"Form");
    static void Mode(string n)=>Call(G,"ChangeMode",Enum.Parse(T("osu.OsuModes"),n),false);
    static void Config(string n,object v){object b=Get(T("osu.Configuration.ConfigManager"),n);Set(b.GetType(),"Value",v,b);}
    static void Wait(int p,double ms=150){phase=p;due=Clock.Elapsed.TotalMilliseconds+ms;}
    static void Check(bool pass,string label){checks++;if(!pass)throw new Exception(label);Records.Add(new{Cycle=cycle,Check=label});}
    static double[] Pointer(){var p=new double[8];if(sr_engine_pointer_state_v1(p,8)!=0)throw new Exception("Pointer state unavailable");return p;}
    static void Raw(int x,int y,uint flags=0)=>Call(Services,"RawPointer",x,y,flags);
    static DisplayState Display=>testDisplay??(DisplayState)Get(T("StableRemastered.Application.ApplicationDisplay"),"Current");
    static void Near(double actual,double expected,string label){Check(Math.Abs(actual-expected)<1.05,label+" actual="+actual+" expected="+expected);}
    static void F9(string page)
    {
        var opts=Get(G,"Options");bool expanded=(bool)Get(opts.GetType(),"Expanded",opts),login=(bool)Get(opts.GetType(),"LoginOnly",opts);
        var chat=T("osu.Online.Social.ChatEngine");
        var method=chat.GetMethod("GameBase_OnKeyRepeat",F);var enumType=method.GetParameters()[1].ParameterType;
        for(int i=0;i<12;i++)method.Invoke(null,new[]{null,Enum.Parse(enumType,"F9"),(object)true});
        Check((bool)Get(opts.GetType(),"Expanded",opts)==expanded&& (bool)Get(opts.GetType(),"LoginOnly",opts)==login,page+": repeated F9 leaves Options state unchanged");
        Check(Get(chat,"Visibility").ToString()=="None",page+": unavailable legacy chat stays hidden");
    }
    static void Start()
    {
        testDisplay=null;
        int[] widths={1280,1024,1280,1280,1280},heights={720,768,800,720,720};
        Config("sRawInput",cycle!=4);Config("sMouseSpeed",cycle==1?1.5:cycle==2?2d:1d);
        Config("sFullscreen",false);Config("sLetterboxing",false);
        Call(G,"SetScreenSize",widths[cycle],heights[cycle],true,false);
        var manager=T("osu.GameplayElements.Beatmaps.BeatmapManager");var map=((IEnumerable)Get(manager,"Beatmaps")).Cast<object>().First(m=>Get(m.GetType(),"Version",m).ToString()=="0 Circles");
        Set(manager,"Current",map);var mt=map.GetType();
        var settings=(GameplaySettings)Call(T("StableRemastered.Application.GameplaySettingsMapper"),"Capture");
        settings.RawInput=cycle!=4;settings.AbsoluteToWindow=cycle!=3;settings.MouseSensitivity=cycle==1?1.5:cycle==2?2:1;
        settings.MaxAssistEnabled=settings.Maple.Enabled=settings.AimCorrection=settings.Relax=settings.SliderTracking=false;
        settings.SliderHealth=settings.SpinnerHealth=false;settings.RecordDiagnostics=settings.RecordReplay=settings.LivePP=false;
        settings.FrameLimit=144;settings.UnlimitedFrames=false;
        string path=Path.GetFullPath(Get(mt,"FilenameFull",map).ToString());
        var accounts=Get(Services,"Accounts");
        context=new StableGameplayContext{Beatmap=new BeatmapRequest{AbsolutePath=path,MD5=Get(mt,"BeatmapChecksum",map).ToString(),Mode=GameplayMode.Standard},Settings=settings,
            MapTitle="Raw Input fixture",Difficulty="0 Circles",ModBits=1,SkinPath=Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"materials","stable-default"),
            Policy=GameplayPolicy.Offline(),AttemptIdentity=Guid.NewGuid(),LocalProfileId=(Guid)Get(accounts.GetType(),"ProfileId",accounts)};
        Set(Services,"PendingQualificationContext",context);Mode("Play");Wait(4,700);
    }
    static void Tick(object sender,EventArgs e)
    {
        if(done||Clock.Elapsed.TotalMilliseconds<due)return;
        try{
            if(Clock.Elapsed.TotalSeconds>100)throw new TimeoutException("Raw Input fixture phase "+phase);
            engine=Get(Services,"Gameplay") as IGameplayEngine;
            if(engine==null)return;
            switch(phase){
            case 0:
                if(Get(G,"Mode").ToString()!="Menu" || Get(T("osu.GameplayElements.Beatmaps.BeatmapManager"),"Beatmaps")==null)return;
                // A desktop observer activates/clicks this isolated window, then
                // writes this marker. No OS input is generated by this runner.
                if(!File.Exists("raw-input-ready.txt"))return;
                if(UpdateOnly){Call(T("StableRemastered.Application.Shell.ShellPresentation"),"ShowClientUpdates");Wait(21,700);break;}
                Config("sRawInput",true);Config("sAbsoluteToOsuWindow",true);Config("sMouseSpeed",1d);Wait(24,500);break;
            case 24:
                Check((bool)Get(Services,"NativePointerAvailable"),"Native raw cursor is available before the first map");Raw(32768,32768,1);Wait(25);break;
            case 25:
                before=Pointer();Near(before[0],Display.LogicalWidth*.5,"Main raw absolute X");Near(before[1],Display.LogicalHeight*.5,"Main raw absolute Y");
                var menuMouse=Get(T("osu.Input.Handlers.MouseManager"),"MousePosition");Near((float)Get(menuMouse.GetType(),"X",menuMouse),before[0],"Main reads the same native raw X");Near((float)Get(menuMouse.GetType(),"Y",menuMouse),before[1],"Main reads the same native raw Y");
                Raw(12,-8);Wait(26);break;
            case 26:
                var mainPointer=Pointer();Near(mainPointer[0],before[0]+12/Display.ScaleX,"Main raw relative X");Near(mainPointer[1],before[1]-8/Display.ScaleY,"Main raw relative Y");F9("Main");Mode("SelectPlay");Wait(1,400);break;
            case 1:
                if(Get(G,"Mode").ToString()!="SelectPlay")return;
                F9("Song Select");var opts=Get(G,"Options");Set(opts.GetType(),"Expanded",true,opts);Wait(2);break;
            case 2:
                F9("Options");var o=Get(G,"Options");Set(o.GetType(),"Expanded",false,o);Wait(3);break;
            case 3:Start();break;
            case 4:
                if(engine.Snapshot.State!=GameplayState.Playing)return;
                F9("Gameplay");Check(Pointer()[6]==0,"Shell hidden-cursor state reaches the engine");Check(Pointer()[7]==1,"Gameplay owns pointer");
                // Exercise the real native presentation boundary at 2x scale
                // and with a centred viewport, without changing monitor modes.
                if(cycle==2||cycle==3){int w=cycle==2?640:1024,h=cycle==2?400:576;engine.ResizeSurface(w,h,1);testDisplay=DisplayState.Create(Form.ClientSize.Width,Form.ClientSize.Height,w,h,cycle==3);engine.SetDisplay(testDisplay);}
                Records.Add(new{Cycle=cycle,Display=Display,Sensitivity=context.Settings.MouseSensitivity,Raw=context.Settings.RawInput});
                if(cycle==3){Raw(32768,32768,1);Wait(16);break;}
                if(cycle==4){before=Pointer();Raw(120,-90);Wait(18);break;}
                Raw(32768,32768,1);Wait(5);break;
            case 5:
                before=Pointer();Near(before[0],Display.LogicalWidth*.5,"Absolute-to-window centre X");Near(before[1],Display.LogicalHeight*.5,"Absolute-to-window centre Y");
                // Multiple packets between game ticks must accumulate once.
                for(int i=0;i<4;i++)Raw(3,-2);
                expectedX=before[0]+12*context.Settings.MouseSensitivity/Display.ScaleX;expectedY=before[1]-8*context.Settings.MouseSensitivity/Display.ScaleY;Wait(6);break;
            case 6:
                var p=Pointer();Near(p[0],expectedX,"Relative packet sum X");Near(p[1],expectedY,"Relative packet sum Y");
                before=p;Raw(-12,8);Wait(7);break;
            case 7:
                p=Pointer();Near(p[0],Display.LogicalWidth*.5,"Reverse movement X");Near(p[1],Display.LogicalHeight*.5,"Reverse movement Y");
                Raw(0,0,1);Wait(8);break;
            case 8:
                expectedX=Display.LogicalWidth*.5*(1-context.Settings.MouseSensitivity);expectedY=Display.LogicalHeight*.5*(1-context.Settings.MouseSensitivity);
                p=Pointer();Near(p[0],expectedX,"Absolute (0,0) is valid X");Near(p[1],expectedY,"Absolute (0,0) is valid Y");Raw(0,0,0);Wait(9);break;
            case 9:
                p=Pointer();Near(p[0],expectedX,"Button-only packet retains tablet X");Near(p[1],expectedY,"Button-only packet retains tablet Y");Raw(32768,32768,1);Wait(10);break;
            case 10:
                engine.Pause();Wait(11);break;
            case 11:
                if(engine.Snapshot.State!=GameplayState.Paused)return;
                F9("Pause");Raw(20000,-20000);engine.SetFocus(false,false);Raw(20000,-20000);engine.SetFocus(true,false);engine.Resume();Wait(12,400);break;
            case 12:
                if(engine.Snapshot.State!=GameplayState.Playing)return;
                if(Pointer()[7]==0){engine.SetKeyState(context.Settings.LeftKey,true);engine.SetKeyState(context.Settings.LeftKey,false);Wait(12,180);break;}
                Raw(32768,32768,1);Wait(13);break;
            case 13:
                p=Pointer();Near(p[0],Display.LogicalWidth*.5,"Pause/focus handoff has no stale delta X");Near(p[1],Display.LogicalHeight*.5,"Pause/focus handoff has no stale delta Y");
                before=p;Raw(7,-5);expectedX=p[0]+7*context.Settings.MouseSensitivity/Display.ScaleX;expectedY=p[1]-5*context.Settings.MouseSensitivity/Display.ScaleY;Wait(14);break;
            case 14:
                p=Pointer();Near(p[0],expectedX,"Relative control after pause X");Near(p[1],expectedY,"Relative control after pause Y");epoch=engine.Snapshot.ClockEpoch;engine.Retry();Wait(15,500);break;
            case 15:
                if(engine.Snapshot.State!=GameplayState.Playing||engine.Snapshot.ClockEpoch==epoch)return;
                Raw(32768,32768,1);Wait(19);break;
            case 16:
                p=Pointer();var screen=Screen.PrimaryScreen.Bounds;var origin=Form.PointToScreen(System.Drawing.Point.Empty);
                Near(p[0],Display.ToLogicalX(screen.Left+screen.Width*.5-origin.X),"Primary-screen tablet X");Near(p[1],Display.ToLogicalY(screen.Top+screen.Height*.5-origin.Y),"Primary-screen tablet Y");
                Raw(32768,32768,3);Wait(17);break;
            case 17:
                p=Pointer();screen=SystemInformation.VirtualScreen;origin=Form.PointToScreen(System.Drawing.Point.Empty);
                Near(p[0],Display.ToLogicalX(screen.Left+screen.Width*.5-origin.X),"Virtual-desktop tablet X");Near(p[1],Display.ToLogicalY(screen.Top+screen.Height*.5-origin.Y),"Virtual-desktop tablet Y");EndCycle();break;
            case 18:
                p=Pointer();Near(p[0],p[2],"Raw off follows OS X");Near(p[1],p[3],"Raw off follows OS Y");EndCycle();break;
            case 19:
                p=Pointer();Near(p[0],Display.LogicalWidth*.5,"Retry seeds new pointer X");Near(p[1],Display.LogicalHeight*.5,"Retry seeds new pointer Y");EndCycle();break;
            case 20:
                if(Get(G,"Mode").ToString()!="SelectPlay")return;
                F9("Return to Song Select");if(cycle==5){Call(T("StableRemastered.Application.Shell.ShellPresentation"),"ShowClientUpdates");Wait(21,700);break;}Wait(3);break;
            case 21:
                var dialog=Get(G,"ActiveDialog");Check(dialog!=null&&Get(dialog.GetType(),"Heading",dialog).ToString()=="Updates","Updates page is reachable after gameplay");
                var updater=Get(Services,"ClientUpdates");updater.GetType().GetMethod("Check",F).Invoke(updater,null);Wait(22,500);break;
            case 22:
                updater=Get(Services,"ClientUpdates");var updateStatus=Get(updater.GetType(),"Status",updater);string updateState=(string)Get(updateStatus.GetType(),"State",updateStatus);if(updateState=="Checking")return;
                Check(updateState=="Current"||updateState=="Available","Live GitHub release check completed: "+Get(updateStatus.GetType(),"Message",updateStatus));
                Records.Add(new{UpdateState=updateState,LiveGitHubCheck=true});Wait(23,6000);break;
            case 23:if(UpdateOnly&&!File.Exists("update-preview-done.txt"))return;Finish(null);break;
            }
        }catch(Exception error){Finish(error.ToString());}
    }
    static void EndCycle(){engine.Stop();Mode("SelectPlay");cycle++;Wait(20,400);}
    static void Finish(string error)
    {
        done=true;timer.Stop();Directory.CreateDirectory("raw-input-evidence");File.WriteAllText("raw-input-evidence/summary.json",JsonConvert.SerializeObject(new{Passed=error==null,Error=error,Checks=checks,Cycles=cycle,ElapsedMs=Clock.Elapsed.TotalMilliseconds,Records,Scope="Production application/engine, synthetic relative and absolute raw packets. Not a physical tablet/mouse feel certificate."},Formatting.Indented));
        if(Form!=null)Form.Close();
    }
    [STAThread]static int Main()
    {
        if(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Name!="raw-input-qualification")return 2;
        Environment.SetEnvironmentVariable("BOSH_TEST_MONITOR",@"\\.\DISPLAY2");
        timer=new Timer{Interval=40};timer.Tick+=Tick;Application.AddMessageFilter(new StartTimerFilter());
        try{Client.EntryPoint.Invoke(null,new object[]{new string[0]});}catch(Exception e){Finish(e.ToString());return 1;}
        return done&&File.ReadAllText("raw-input-evidence/summary.json").Contains("\"Passed\": true")?0:1;
    }
}
