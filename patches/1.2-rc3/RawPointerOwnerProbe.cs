// Read-only transport observation in a disposable client. No input is generated.
using System;using System.IO;using System.Linq;using System.Reflection;using System.Collections;using System.Linq.Expressions;using System.Runtime.InteropServices;using System.Windows.Forms;using Newtonsoft.Json;using StableRemastered.Gameplay;
class RawPointerOwnerProbe:IMessageFilter
{
 const BindingFlags F=BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Static|BindingFlags.Instance;
 static readonly Assembly Client=typeof(GameplaySettings).Assembly;static Timer timer;static bool started,configured;static long messages,packets;static object last,lastMovement;
 [StructLayout(LayoutKind.Sequential)]struct Device{public ushort Page,Usage;public uint Flags;public IntPtr Target;}
 [DllImport("user32.dll",SetLastError=true)]static extern uint GetRegisteredRawInputDevices([Out]Device[] devices,ref uint count,uint size);
 static Type T(string n)=>Client.GetType(n,true);static object Get(Type t,string n,object o=null)=>t.GetProperty(n,F)?.GetValue(o)??t.GetField(n,F)?.GetValue(o);static object Get(object o,string n)=>o==null?null:Get(o.GetType(),n,o);
 static void Raw(object value){packets++;var m=Get(value,"Mouse");var form=Get(T("osu.GameBase"),"Form") as Form;last=new{X=Get(m,"LastX"),Y=Get(m,"LastY"),Flags=Get(m,"Flags").ToString(),Device=Get(Get(value,"Header"),"Device"),OS=Cursor.Position,Origin=form.PointToScreen(System.Drawing.Point.Empty),Primary=Screen.PrimaryScreen.Bounds,Desktop=SystemInformation.VirtualScreen};if((int)Get(m,"LastX")!=0 || (int)Get(m,"LastY")!=0)lastMovement=last;}
 public bool PreFilterMessage(ref Message m){if(!started){started=true;timer.Start();}if(m.Msg==255)messages++;return false;}
 static void Tick(object sender,EventArgs args)
 {
  try{
   var form=Get(T("osu.GameBase"),"Form") as Form;if(form==null)return;
   if(!configured && Get(T("osu.GameBase"),"Mode").ToString()=="Menu" && File.Exists("probe-ready.txt")){
    var setting=Get(T("osu.Configuration.ConfigManager"),"sRawInput");setting.GetType().GetProperty("Value",F).SetValue(setting,true);
    var absolute=Get(T("osu.Configuration.ConfigManager"),"sAbsoluteToOsuWindow");absolute.GetType().GetProperty("Value",F).SetValue(absolute,false);
    var mouseEvent=T("osu.Input.Handlers.WndProcInputHandler").GetEvent("OnMouse",F);var p=Expression.Parameter(mouseEvent.EventHandlerType.GetMethod("Invoke").GetParameters()[0].ParameterType);var handler=Expression.Lambda(mouseEvent.EventHandlerType,Expression.Call(typeof(RawPointerOwnerProbe).GetMethod("Raw",F),Expression.Convert(p,typeof(object))),p).Compile();mouseEvent.GetAddMethod(true).Invoke(null,new object[]{handler});configured=true;
   }
   if(!configured)return;
   uint count=0,size=(uint)Marshal.SizeOf(typeof(Device));GetRegisteredRawInputDevices(null,ref count,size);var devices=new Device[count];GetRegisteredRawInputDevices(devices,ref count,size);
   var input=T("osu.Input.InputManager");var getHandler=input.GetMethod("GetHandler",F);var raw=getHandler.Invoke(null,new object[]{T("osu.Input.Handlers.RawMouseHandler")});var normal=getHandler.Invoke(null,new object[]{T("osu.Input.Handlers.MouseHandler")});var mouse=T("osu.Input.Handlers.MouseManager");var host=Get(T("osu.GameBase"),"Instance");
   File.WriteAllText("probe.json",JsonConvert.SerializeObject(new{Configured=configured,Messages=messages,Packets=packets,Last=last,LastMovement=lastMovement,Mode=Get(T("osu.GameBase"),"Mode").ToString(),FormHandle=form.Handle.ToInt64(),Devices=devices.Select(d=>new{d.Page,d.Usage,d.Flags,Target=d.Target.ToInt64()}),Active=Get(host,"IsActive"),RawActive=Get(raw,"IsActive"),RawPosition=Get(raw,"Position"),RawAbsolute=Get(raw,"AbsoluteMovement"),NormalPosition=Get(normal,"Position"),CurrentHandler=Get(input,"CurrentCursorHandler")?.GetType().Name,Cursor=Get(mouse,"MousePosition"),ShowMouse=Get(mouse,"showMouse"),ShowWindow=Get(mouse,"showWindow")},Formatting.Indented));
   if(File.Exists("probe-close.txt")){timer.Stop();form.Close();}
  }catch(Exception e){File.WriteAllText("probe-error.txt",e.ToString());}
 }
 [STAThread]static void Main(){Environment.SetEnvironmentVariable("BOSH_TEST_MONITOR",@"\\.\DISPLAY2");timer=new Timer{Interval=250};timer.Tick+=Tick;Application.AddMessageFilter(new RawPointerOwnerProbe());Client.EntryPoint.Invoke(null,new object[]{new string[0]});}
}
