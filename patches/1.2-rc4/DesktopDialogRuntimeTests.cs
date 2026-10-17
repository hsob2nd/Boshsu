// Real-client scope tests and read-only Windows cursor observation. Actual file
// selection/cancellation is performed through the desktop testing tool.
using System;using System.IO;using System.Reflection;using System.Linq;using System.Collections;using System.Collections.Generic;using System.Windows.Forms;using System.Runtime.InteropServices;using System.Diagnostics;using Newtonsoft.Json;using StableRemastered.Gameplay;
class DesktopDialogRuntimeTests
{
 const BindingFlags F=BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.Instance;
 static readonly Assembly Client=typeof(GameplaySettings).Assembly;static readonly List<string> checks=new List<string>();static readonly List<object> modal=new List<object>();static Timer timer;static int phase,cycle;static double due;static bool done;static string page="",failure;
 static readonly Stopwatch clock=Stopwatch.StartNew();
 [StructLayout(LayoutKind.Sequential)]struct CursorState{public int Size,Flags;public IntPtr Cursor;public System.Drawing.Point Position;}
 [DllImport("user32.dll")]static extern bool GetCursorInfo(ref CursorState state);
 static Type T(string name)=>Client.GetType(name,true);
 static object Get(Type t,string n,object o=null)=>t.GetProperty(n,F)?.GetValue(o)??t.GetField(n,F)?.GetValue(o);
 static object Get(object o,string n)=>Get(o.GetType(),n,o);
 static object Call(Type t,string n,params object[] a)=>t.GetMethods(F).Single(m=>m.Name==n&&m.GetParameters().Length==a.Length).Invoke(null,a);
 static Type G=>T("osu.GameBase");static Type S=>T("StableRemastered.Application.ApplicationServices");static Type D=>T("StableRemastered.Application.DesktopFilePicker");static Type M=>T("osu.Input.Handlers.MouseManager");static Form Owner=>(Form)Get(G,"Form");
 static void Config(string name,object value){var b=Get(T("osu.Configuration.ConfigManager"),name);b.GetType().GetProperty("Value",F).SetValue(b,value);}
 static object Config(string name)=>Get(Get(T("osu.Configuration.ConfigManager"),name),"Value");
 static bool Open=>(bool)Get(D,"IsOpen");
 static void Check(bool pass,string label){if(!pass)throw new Exception(label);checks.Add(label);}
 static bool CursorVisible(){var state=new CursorState{Size=Marshal.SizeOf(typeof(CursorState))};if(!GetCursorInfo(ref state))throw new Exception("GetCursorInfo failed");return (state.Flags&1)!=0;}
 static DialogResult Run(Func<IWin32Window,DialogResult> action){try{return (DialogResult)D.GetMethod("Run",F).MakeGenericMethod(typeof(DialogResult)).Invoke(null,new object[]{action});}catch(TargetInvocationException e){throw e.InnerException;}}
 static void Inside(IWin32Window owner)
 {
  Check(owner.Handle==Owner.Handle,"Picker belongs to the main client window");Check(Open,"Desktop ownership active");Check((bool)Get(G,"MouseVisible")&&CursorVisible(),"Windows cursor is visible");Check((bool)Get(M,"showWindow")&&!(bool)Get(M,"showGame"),"Shell cursor yields to Windows");Check(!(bool)Get(S,"UsesNativeRawPointer"),"Native raw cursor ownership is suspended");Check(!Owner.TopMost&&!(bool)Get(G,"TopMost"),"Fullscreen cannot cover picker");Check(Cursor.Clip==(System.Drawing.Rectangle)Get(T("osu.Input.InputManager"),"CursorOriginalBounds"),"Cursor confinement released");
  Call(T("osu.Input.InputManager"),"UpdateShallClampMouseToWindow");Check(!(bool)Get(T("osu.Input.InputManager"),"ShallClampMouseToWindow"),"Nested updates cannot re-confine cursor");
  var host=Get(typeofGame(),"host",Get(G,"Instance"));host.GetType().GetMethod("onacApplicationIdle",F).Invoke(host,new object[]{null,EventArgs.Empty});Check(Open,"Nested modal idle leaves desktop ownership intact");
 }
 static Type typeofGame()=>T("osu.Framework.Game");
 static void Contracts()
 {
  var fullscreen=Config("sFullscreen");var raw=Config("sRawInput");var speed=Config("sMouseSpeed");
  Check(Run(owner=>{Inside(owner);Check(Run(inner=>{Check(inner.Handle==owner.Handle,"Nested picker retains owner");return DialogResult.Cancel;})==DialogResult.Cancel&&Open,"Nested cancellation retains outer handoff");return DialogResult.Cancel;})==DialogResult.Cancel,"Cancellation returned");
  Check(!Open,"Cancel restores game ownership");
  Check(Run(owner=>{Inside(owner);return DialogResult.OK;})==DialogResult.OK&&!Open,"Selection restores game ownership");
  bool threw=false;try{Run(owner=>{Inside(owner);throw new IOException("Fixture picker failure");});}catch(IOException){threw=true;}
  Check(threw&&!Open,"Picker failure restores game ownership");
  Check(Equals(fullscreen,Config("sFullscreen"))&&Equals(raw,Config("sRawInput"))&&Equals(speed,Config("sMouseSpeed")),"Display and input preferences unchanged");
  Check(Owner.TopMost==(bool)Get(G,"TopMost"),"Fullscreen priority restored according to focus");
  Check(CursorVisible()==(bool)Get(G,"MouseVisible"),"Repeated handoffs balance Windows cursor visibility");
 }
 static void Tick(object sender,EventArgs e)
 {
  if(done)return;
  try{
   if(Open){bool visible=CursorVisible();bool raw=(bool)Get(S,"UsesNativeRawPointer");bool clipped=(bool)Get(T("osu.Input.InputManager"),"ShallClampMouseToWindow");if(!visible||raw||clipped||Owner.TopMost)failure="Actual modal picker retained game cursor/focus ownership";if(modal.Count<300)modal.Add(new{Page=page,WindowsCursorVisible=visible,RawOwner=raw,Clipped=clipped,OwnerTopMost=Owner.TopMost});return;}
   if(clock.Elapsed.TotalMilliseconds<due)return;
   if(phase==0){if(Get(G,"Mode").ToString()!="Menu")return;Config("sRawInput",true);Config("sAbsoluteToOsuWindow",false);due=clock.Elapsed.TotalMilliseconds+900;phase=1;return;}
   if(phase==1){if(!(bool)Get(S,"NativePointerAvailable"))return;Config("sRawInput",cycle%2==0);Config("sFullscreen",cycle<2);Call(G,"SetScreenSize",cycle<2?1920:1280,cycle<2?1080:720,true,true);due=clock.Elapsed.TotalMilliseconds+1000;phase=2;return;}
   if(phase==2){Contracts();cycle++;if(cycle<4){phase=1;return;}Config("sRawInput",true);Config("sFullscreen",true);Call(G,"SetScreenSize",1920,1080,true,true);phase=3;due=clock.Elapsed.TotalMilliseconds+1200;return;}
   string command=File.Exists("dialog-page.txt")?File.ReadAllText("dialog-page.txt").Trim():"avatar";
   if(command=="finish"){
    Check(failure==null,failure??"Actual pickers release the Windows cursor");Check(modal.Count>0,"Observed real Windows file picker");
    Finish(null);return;
   }
   if(command!=page){var active=Get(G,"ActiveDialog");if(active!=null){var close=active.GetType().GetMethod("Close",F);close.Invoke(active,close.GetParameters().Select(p=>p.HasDefaultValue?p.DefaultValue:(object)false).ToArray());}var shell=T("StableRemastered.Application.Shell.ShellPresentation");Call(shell,command=="pp"?"ShowPPSystems":"ShowIdentity",command=="pp"?new object[]{0}:new object[0]);page=command;}
   var accounts=Get(S,"Accounts");var store=Get(accounts,"Store");var profiles=(IEnumerable)store.GetType().GetMethod("Profiles",F).Invoke(store,null);var current=profiles.Cast<object>().First(p=>Equals(Get(p,"Id"),Get(accounts,"ProfileId")));
   var listings=((IEnumerable)Get(Get(S,"PPProviders"),"Listings")).Cast<object>().Select(x=>Get(x,"Label")).ToArray();
   File.WriteAllText("dialog-evidence/current.json",JsonConvert.SerializeObject(new{Phase=phase,Cycles=cycle,Checks=checks.Count,Page=page,RawInput=Config("sRawInput"),Fullscreen=Config("sFullscreen"),ModalSamples=modal.Count,Failure=failure,Avatar=Get(current,"AvatarPath"),Providers=listings},Formatting.Indented));
  }catch(Exception error){Finish(error.ToString());}
 }
 static void Finish(string error){done=true;timer.Stop();File.WriteAllText("dialog-evidence/summary.json",JsonConvert.SerializeObject(new{Passed=error==null,Error=error,Checks=checks.Count,Cycles=cycle,ActualModalSamples=modal.Count,ChecksList=checks,Modal=modal,PlayerDataUsed=false},Formatting.Indented));Owner?.Close();}
 sealed class Start:IMessageFilter{bool started;public bool PreFilterMessage(ref Message m){if(!started){started=true;timer.Start();}return false;}}
 [STAThread]static int Main(){if(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Name!="desktop-dialog-qualification")return 2;Environment.CurrentDirectory=AppDomain.CurrentDomain.BaseDirectory;Directory.CreateDirectory("dialog-evidence");File.WriteAllText("dialog-evidence/launched.txt",DateTime.UtcNow.ToString("o"));Environment.SetEnvironmentVariable("BOSH_TEST_MONITOR",@"\\.\DISPLAY2");timer=new Timer{Interval=200};timer.Tick+=Tick;Application.AddMessageFilter(new Start());Client.EntryPoint.Invoke(null,new object[]{new string[0]});return done?0:1;}
}
