// Disposable real-client fixture. Uses the production window-message route,
// pause controls and native runtime; does not qualify physical device feel.
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json;
using StableRemastered.Gameplay;
using StableRemastered.Accounts;
class PauseRuntimeTests
{
    const BindingFlags F=BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.Instance;
    static readonly Assembly Client=typeof(GameplaySettings).Assembly;
    static readonly Stopwatch Clock=Stopwatch.StartNew();static readonly List<object> Records=new List<object>();
    static Timer timer;static int phase,cycle,checks;static double due;static bool done;
    static IGameplayEngine engine;static StableGameplayContext context;static GameplaySnapshot frozen;static double beforeResume;
    static Type T(string n)=>Client.GetType(n,true);static Type G=>T("osu.GameBase");static Type S=>T("StableRemastered.Application.ApplicationServices");
    static object Get(Type t,string n,object o=null)=>t.GetProperty(n,F)?.GetValue(o)??t.GetField(n,F)?.GetValue(o);
    static void Set(Type t,string n,object v,object o=null){var p=t.GetProperty(n,F);if(p!=null)p.SetValue(o,v);else t.GetField(n,F).SetValue(o,v);}
    static object Call(Type t,string n,params object[] a)=>t.GetMethods(F).Single(m=>m.Name==n&&m.GetParameters().Length==a.Length).Invoke(null,a);
    static Form Form=>(Form)Get(G,"Form");static object ModeObject=>Get(G,"RunningGameMode");
    static void Mode(string n)=>Call(G,"ChangeMode",Enum.Parse(T("osu.OsuModes"),n),false);
    static void Config(string n,object v){var b=Get(T("osu.Configuration.ConfigManager"),n);Set(b.GetType(),"Value",v,b);}
    static void Check(bool pass,string label){++checks;Records.Add(new{Cycle=cycle,Check=label,Passed=pass,State=engine?.Snapshot.State.ToString(),Time=engine?.Snapshot.TimeMs});if(!pass)throw new Exception(label);}
    static void Wait(int p,double delay=150){phase=p;due=Clock.Elapsed.TotalMilliseconds+delay;}
    static void Escape(bool repeat=false)
    {
        var message=Message.Create(Form.Handle,0x100,new IntPtr(27),repeat?new IntPtr(1L<<30):IntPtr.Zero);
        var method=Form.GetType().GetMethod("ProcessCmdKey",F);Check((bool)method.Invoke(Form,new object[]{message,Keys.Escape}),"Form owns Escape / repeat="+repeat);
        if(!repeat)Call(S,"HandleGameplayKey",27,false,false);
    }
    static void Click(string field)
    {
        var mode=ModeObject;var sprite=Get(mode.GetType(),field,mode);Type t=sprite.GetType();FieldInfo click=null;
        while(t!=null&&click==null){click=t.GetField("OnClick",F|BindingFlags.DeclaredOnly);t=t.BaseType;}
        ((EventHandler)click.GetValue(sprite))(sprite,EventArgs.Empty);
    }
    static void Frozen(string label)
    {
        var s=engine.Snapshot;Check(s.State==GameplayState.Paused,label+" remains paused");
        Check(s.TimeMs==frozen.TimeMs,label+" clock frozen");Check(s.Score==frozen.Score&&s.Count300==frozen.Count300&&s.Count100==frozen.Count100&&s.Count50==frozen.Count50&&s.CountMiss==frozen.CountMiss,label+" score/judgments frozen");
        Check((bool)Get(ModeObject.GetType(),"QualificationPausedControlsActive",ModeObject),label+" pause controls visible and active");
    }
    static void Start()
    {
        var manager=T("osu.GameplayElements.Beatmaps.BeatmapManager");var map=((IEnumerable)Get(manager,"Beatmaps")).Cast<object>().First(m=>Get(m.GetType(),"Version",m).ToString()=="0 Circles");Set(manager,"Current",map);
        var settings=(GameplaySettings)Call(T("StableRemastered.Application.GameplaySettingsMapper"),"Capture");
        settings.RawInput=cycle%2==1;settings.MaxAssistEnabled=cycle==2;settings.Maple.Enabled=cycle==1||cycle==3;settings.Maple.Version=(MapleVersion)(cycle==3?2:0);settings.Maple.V1Strength=1;settings.Maple.V3Power=settings.Maple.V3SliderPower=2;
        settings.AimCorrection=cycle>=2;settings.Relax=cycle>0;settings.Rate=cycle%2==1?1.95:1;settings.RecordReplay=settings.RecordDiagnostics=settings.LivePP=false;settings.UnlimitedFrames=true;settings.SliderHealth=settings.SpinnerHealth=false;
        var accounts=Get(S,"Accounts");context=new StableGameplayContext{Beatmap=new BeatmapRequest{AbsolutePath=Path.GetFullPath(Get(map.GetType(),"FilenameFull",map).ToString()),MD5=Get(map.GetType(),"BeatmapChecksum",map).ToString(),Mode=GameplayMode.Standard},Settings=settings,MapTitle="Owned pause fixture",Difficulty="0 Circles",ModBits=1,SkinPath=Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"materials","stable-default"),Policy=GameplayPolicy.Offline(),AttemptIdentity=Guid.NewGuid(),LocalProfileId=(Guid)Get(accounts.GetType(),"ProfileId",accounts)};
        Set(S,"PendingQualificationContext",context);Mode("Play");Wait(2,300);
    }
    static void Tick(object sender,EventArgs args)
    {
        if(done||Clock.Elapsed.TotalMilliseconds<due)return;
        try{
            if(Clock.Elapsed.TotalSeconds>100)throw new TimeoutException("Pause test phase "+phase);
            engine=Get(S,"Gameplay") as IGameplayEngine;if(engine==null)return;
            switch(phase){
            case 0:
                if(Get(G,"Mode").ToString()!="Menu"||Get(T("osu.GameplayElements.Beatmaps.BeatmapManager"),"Beatmaps")==null)return;
                Config("sFullscreen",false);Call(G,"SetScreenSize",1280,720,true,false);Mode("SelectPlay");Wait(1,500);break;
            case 1:if(Get(G,"Mode").ToString()!="SelectPlay")return;Start();break;
            case 2:
                if(engine.Snapshot.State!=GameplayState.Playing||engine.Snapshot.TimeMs<2500)return;
                Escape();for(int i=0;i<8;i++)Escape(true);Wait(3,200);break;
            case 3:
                Check(engine.Snapshot.State==GameplayState.Paused,"Escape pauses native gameplay");frozen=engine.Snapshot;
                engine.SetKeyState(context.Settings.LeftKey,true);engine.SetKeyState(context.Settings.LeftKey,false);engine.SetPointerButton(0,true);engine.SetPointerButton(0,false);Wait(4,750);break;
            case 4:Frozen("Initial pause with tapping");Click("resume");Wait(5,400);break;
            case 5:
                Check(engine.Snapshot.State==GameplayState.Playing,"Continue leaves menu");beforeResume=engine.Snapshot.TimeMs;
                // Reopen the menu while waiting for the continuation click.
                Escape();Wait(6,200);break;
            case 6:
                Check(engine.Snapshot.State==GameplayState.Paused,"Re-pause during continuation");frozen=engine.Snapshot;
                engine.SetKeyState(context.Settings.LeftKey,true);engine.SetKeyState(context.Settings.LeftKey,false);engine.SetPointerButton(0,true);engine.SetPointerButton(0,false);Wait(7,750);break;
            case 7:Frozen("Paused continuation with tapping");Click("resume");Wait(8,500);break;
            case 8:engine.SetKeyState(context.Settings.LeftKey,true);engine.SetKeyState(context.Settings.LeftKey,false);beforeResume=engine.Snapshot.TimeMs;Wait(9,300);break;
            case 9:Check(engine.Snapshot.State==GameplayState.Playing&&engine.Snapshot.TimeMs>beforeResume,"Explicit Continue and confirmation advance gameplay");engine.Pause();Wait(10,200);break;
            case 10:
                Check(engine.Snapshot.State==GameplayState.Paused,"Direct pause command");frozen=engine.Snapshot;engine.SetFocus(false,false);engine.SetFocus(true,false);Wait(11,600);break;
            case 11:Frozen("Focus regain");Click("retry");Wait(12,400);break;
            case 12:
                if(engine.Snapshot.State!=GameplayState.Playing)return;
                Escape();Wait(13,200);break;
            case 13:Check(engine.Snapshot.State==GameplayState.Paused,"Retry can be paused");Click("back");Wait(14,300);break;
            case 14:
                if(Get(G,"Mode").ToString()!="SelectPlay")return;
                if(++cycle==4)Finish(null);else Wait(1,300);break;
            }
        }catch(Exception error){Finish(error.ToString());}
    }
    static void Finish(string error){done=true;timer.Stop();Directory.CreateDirectory("pause-evidence");File.WriteAllText("pause-evidence/summary.json",JsonConvert.SerializeObject(new{Passed=error==null,Error=error,Checks=checks,Cycles=cycle,ElapsedMs=Clock.Elapsed.TotalMilliseconds,Records},Formatting.Indented));Form?.Close();}
    sealed class StartFilter:IMessageFilter{bool started;public bool PreFilterMessage(ref Message m){if(!started){started=true;timer.Start();}return false;}}
    [STAThread]static int Main(){if(!new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Name.EndsWith("pause-qualification"))return 2;Environment.SetEnvironmentVariable("BOSH_TEST_MONITOR",@"\\.\DISPLAY2");timer=new Timer{Interval=35};timer.Tick+=Tick;Application.AddMessageFilter(new StartFilter());try{Client.EntryPoint.Invoke(null,new object[]{new string[0]});}catch(Exception e){Finish(e.ToString());return 1;}return done&&File.ReadAllText("pause-evidence/summary.json").Contains("\"Passed\": true")?0:1;}
}
