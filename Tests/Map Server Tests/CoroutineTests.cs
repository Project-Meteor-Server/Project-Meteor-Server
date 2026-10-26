using System;
using System.Collections.Generic;
using System.Linq;
using Meteor.Map.lua;
using Xunit;

namespace Meteor.Map.Tests
{
    // Checks that scripts can pause and carry on (how wait(), waitForSignal() and NPC
    // dialogue work), that script errors are reported, and that waiting scripts wake up on time.
    [Collection("engines")]
    public sealed class CoroutineTests
    {
        private readonly EngineFixture engines;
        const string Path = "./scripts/__tests/coroutines.lua";

        public CoroutineTests(EngineFixture engines)
        {
            this.engines = engines;
        }


        [Fact]
        public void YieldProtocol()
        {
            var probe = new Probe();
            var co = engines.Engine.Load(Path).CreateCoroutine("flow");

            var r = co.Resume(probe, "b");
            Assert.True(r.Yielded);
            Assert.Equal(new object[] { "_WAIT_TIME", 0.25 }, r.Values);

            r = co.Resume(11.0);
            Assert.Equal(new object[] { "_WAIT_SIGNAL", "go" }, r.Values);

            r = co.Resume("x", null);
            Assert.True(r.Yielded);
            Assert.Equal("_WAIT_EVENT", r[0]);
            Assert.Same(probe, r[1]);

            r = co.Resume(true);
            Assert.False(r.Yielded);
            Assert.True(co.IsDead);
            Assert.Equal(new object[] { "done", "b", 11.0, "x", null, true }, r.Values);
        }

        [Fact]
        public void ScriptErrorsCarryFileAndLine()
        {
            var script = engines.Engine.Load(Path);
            var e = Assert.Throws<LuaScriptException>(() => script.CreateCoroutine("boom").Resume());
            Assert.Contains("coroutines.lua", e.DecoratedMessage);
            Assert.Contains("coroutines.lua:11:", e.DecoratedMessage);

            var late = script.CreateCoroutine("boomAfterYield");
            Assert.True(late.Resume().Yielded);
            var e2 = Assert.Throws<LuaScriptException>(() => late.Resume());
            Assert.Contains("late failure", e2.DecoratedMessage);

            // A normal (non-pausing) call that fails returns null and logs the error.
            Assert.Null(script.Call("boom"));
        }

        [Fact]
        public void TimerWakesWaitOnTime()
        {
            var lua = LuaEngine.GetInstance();
            var probe = new Probe();
            var co = engines.Engine.Load(Path).CreateCoroutine("sleeper");

            long start = Environment.TickCount64;
            lua.ResolveResume(null, co, co.Resume(probe, 0.2));
            Assert.True(probe.WaitForMarks(1, 2000), "wait(0.2) never resumed");
            long elapsed = probe.MarkTimes[0] - start;

            // Never early. The timer checks every 50 ms, so it is normally up to 50 ms late, but shared
            // CI machines sometimes pause for longer (358 ms was seen once on a busy Intel Mac), so
            // only a wake-up a whole second late counts as broken.
            Assert.InRange(elapsed, 195, 1000);
        }

        // One thread holds the zone lock and runs a script (as the zone update does for mobs) while
        // another thread's script asks the world for a zone (which takes the zone lock). Both must
        // finish; with separate zone and script locks the two threads would wait on each other.
        [Fact]
        public void ZoneUpdateAndScriptDoNotDeadlock()
        {
            var script = engines.Engine.Load(Path);
            var probe = new Probe();
            for (int round = 0; round < 20; round++)
            {
                var zoneUpdate = System.Threading.Tasks.Task.Run(() =>
                {
                    lock (WorldManager.ZoneLock)
                    {
                        System.Threading.Thread.Sleep(5);
                        script.Call("touchZones", probe);
                    }
                });
                var packetThread = System.Threading.Tasks.Task.Run(() => script.Call("touchZones", probe));
                Assert.True(System.Threading.Tasks.Task.WaitAll(new[] { zoneUpdate, packetThread }, 5000), "the two threads deadlocked");
            }
        }

        [Fact]
        public void SignalWakesWaiter()
        {
            var lua = LuaEngine.GetInstance();
            var probe = new Probe();
            string signal = "test-" + Guid.NewGuid();
            var co = engines.Engine.Load(Path).CreateCoroutine("waiter");

            lua.ResolveResume(null, co, co.Resume(probe, signal));
            Assert.Equal(0, probe.Marks);
            lua.OnSignal(signal);
            Assert.Equal(1, probe.Marks);
            Assert.True(co.IsDead);
        }
    }
}
