// Compiles the production Logger with a controllable background scheduler.
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StableRemastered.Gameplay;
using osu_common.Helpers;
class FrameLogTests
{
    static int checks;static void Check(bool value,string why){++checks;if(!value)throw new Exception(why);}
    static int Main(string[] args)
    {
        foreach(double target in new[]{6d,8d,16d}){
            Check(!FrameStallPolicy.ShouldReport(target+3,target,6),"Intentional pacing is not a stall");
            Check(FrameStallPolicy.ShouldReport(target+10,target,6),"Real work after pacing remains visible");
            Check(FrameStallPolicy.ShouldReport(target+10,0,6),"Render/swap stall remains visible");
        }
        Check(!FrameStallPolicy.ShouldReport(8.8,8.37,7.2),"Session's limiter-only frame excluded");
        Check(FrameStallPolicy.ShouldReport(10.28,0,6),"Session's gameplay hitch retained");
        Check(FrameStallPolicy.ShouldReport(930.26,0,6),"First attachment retained");
        Check(!FrameStallPolicy.ShouldReport(double.NaN,0,6)&&!FrameStallPolicy.ShouldReport(10,double.PositiveInfinity,6),"Invalid sample rejected");
        Logger.LogDirectory=Path.GetFullPath(args[0]);Logger.Enabled=true;
        var log=Logger.GetLogger(LoggingTarget.Runtime,true);ThreadedScheduler.Last.Drain();
        log.Add("password-filter-fixture",LogLevel.Error);Logger.AddFilteredText("secret-value");log.Add("protected secret-value");ThreadedScheduler.Last.Drain();
        Check(!File.ReadAllText(log.Filename).Contains("secret-value"),"Credential filtering preserved");
        int before=ThreadedScheduler.Last.Enqueued;log.Add("debug-only",LogLevel.Debug);Check(ThreadedScheduler.Last.Enqueued==before,"Bulk debug entries skipped in release");
        Parallel.For(0,4000,i=>Logger.Log("concurrent-row-"+i,LoggingTarget.Runtime));
        ThreadedScheduler.Last.DrainParallel();
        Check(File.ReadAllLines(log.Filename).Count(s=>s.Contains("concurrent-row-"))==4000,"Concurrent queued writes are serialized without lost entries");
        Parallel.For(0,400,i=>Logger.GetLogger(LoggingTarget.Network,true));ThreadedScheduler.Last.Drain();
        Check(File.ReadAllLines(Logger.GetLogger(LoggingTarget.Network).Filename).Count(s=>s.Contains("Network Log"))==1,"Concurrent logger creation clears once");
        for(int i=0;i<2000;i++)log.Add(new string('x',1024));ThreadedScheduler.Last.Drain();
        Check(new FileInfo(log.Filename).Length<=1024*1024,"Hard disk cap preserved");
        before=ThreadedScheduler.Last.Enqueued;for(int i=0;i<100000;i++)log.Add("after-cap");
        Check(ThreadedScheduler.Last.Enqueued==before,"Capped logging stops creating/queuing write work");
        log.Clear();ThreadedScheduler.Last.Drain();log.Add("new-session-record");ThreadedScheduler.Last.Drain();
        Check(File.ReadAllText(log.Filename).Contains("new-session-record")&&!File.ReadAllText(log.Filename).Contains("concurrent-row-"),"Clear resets the cap and previous data");
        Logger.Enabled=false;before=ThreadedScheduler.Last.Enqueued;log.Add("disabled");Check(ThreadedScheduler.Last.Enqueued==before,"Disabled logging stays inert");
        Console.WriteLine(checks+" frame/logging checks passed");return 0;
    }
}
namespace osu_common {public static class OsuCommon {public static System.IFormatProvider nfi=System.Globalization.CultureInfo.InvariantCulture;}}
namespace osu_common.Helpers
{
    public static class GeneralHelper{public static void FileMove(string a,string b){File.Move(a,b);}public static void FileDelete(string p){if(File.Exists(p))File.Delete(p);}}
    public sealed class ThreadedScheduler
    {
        public static ThreadedScheduler Last;public int Enqueued;readonly Queue<Action> queue=new Queue<Action>();
        public ThreadedScheduler(){Last=this;}
        public void Add(Action action,bool force){lock(queue){queue.Enqueue(action);Enqueued++;}}
        public void Drain(){while(true){Action work;lock(queue){if(queue.Count==0)return;work=queue.Dequeue();}work();}}
        public void DrainParallel(){Parallel.For(0,8,i=>Drain());}
    }
}
