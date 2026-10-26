/*
===========================================================================
Copyright (C) 2015-2019 Project Meteor Dev Team

This file is part of Project Meteor Server.

Project Meteor Server is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Project Meteor Server is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Project Meteor Server. If not, see <https:www.gnu.org/licenses/>.
===========================================================================
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Meteor.Map.actors.area;
using Meteor.Navigation;

namespace Meteor.Map.navigation
{
    /// <summary>
    /// The client's collision per zone. All zones together take about 1 GB, so a zone loads in the
    /// background on first use and unloads after UNLOAD_AFTER unused. Near a zone line, probes also ask
    /// the next zones in the region, since the client has those loaded too.
    /// </summary>
    static class CollisionManager
    {
        public static readonly TimeSpan UNLOAD_AFTER = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan SWEEP_EVERY = TimeSpan.FromMinutes(1);
        private const float BOUNDS_SLACK = 1.0f;

        public sealed class Entry
        {
            public string Name;
            public string Dir;
            public System.Numerics.Vector3 Min, Max;
            public volatile CollisionWorld World;
            public long LastUsedMs;
            public int Loads;
            private int loading;
            public bool Failed;

            public bool Covers(float x, float z) =>
                x >= Min.X - BOUNDS_SLACK && x <= Max.X + BOUNDS_SLACK && z >= Min.Z - BOUNDS_SLACK && z <= Max.Z + BOUNDS_SLACK;

            internal bool TryBeginLoad() => !Failed && Interlocked.CompareExchange(ref loading, 1, 0) == 0;
            internal void EndLoad() => Volatile.Write(ref loading, 0);
            public bool Loading => Volatile.Read(ref loading) != 0;
        }

        public enum ProbeResult { Hit, Miss, NoCollision }

        private static readonly Dictionary<string, Entry> entryFor = new Dictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, Entry[]> neighboursOf = new Dictionary<string, Entry[]>(StringComparer.OrdinalIgnoreCase);
        private static readonly List<Entry> entries = new List<Entry>();
        private static Timer sweeper;

        public static string Root { get; private set; }
        public static IReadOnlyList<Entry> Entries => entries;

        public static void FindForZones(IEnumerable<Zone> zones)
        {
            if (!ConfigConstants.NAVMESH_ENABLED)
                return;
            Root = ConfigConstants.COLLISION_PATH;
            var zoneList = zones.ToList();
            Dictionary<string, string> found;
            try
            {
                found = Directory.Exists(Root) ? XcolReader.ResolveZones(Root) : new Dictionary<string, string>();
            }
            catch (Exception e)
            {
                Program.Log.Error("Collision export {0} unreadable: {1}", Path.GetFullPath(Root), e.Message);
                return;
            }

            var byDir = new Dictionary<string, Entry>(StringComparer.OrdinalIgnoreCase);
            foreach (var zone in zoneList)
            {
                if (string.IsNullOrEmpty(zone.ZoneName) || !found.TryGetValue(zone.ZoneName, out var dir))
                    continue;
                if (!byDir.TryGetValue(dir, out var e))
                {
                    try
                    {
                        var (min, max) = XcolReader.ReadBounds(Path.Combine(dir, "static.xcol"));
                        e = new Entry { Name = Path.GetFileName(dir), Dir = dir, Min = min, Max = max };
                    }
                    catch (Exception ex)
                    {
                        Program.Log.Error("Client collision {0} unreadable: {1}", dir, ex.Message);
                        continue;
                    }
                    byDir[dir] = e;
                    entries.Add(e);
                }
                entryFor[zone.ZoneName] = e;
            }
            foreach (var zone in zoneList)
            {
                if (string.IsNullOrEmpty(zone.ZoneName))
                    continue;
                entryFor.TryGetValue(zone.ZoneName, out var own);
                neighboursOf[zone.ZoneName] = zoneList
                    .Where(z => z.RegionId == zone.RegionId && z.ZoneName != null && entryFor.ContainsKey(z.ZoneName))
                    .Select(z => entryFor[z.ZoneName]).Where(e => e != own).Distinct().ToArray();
            }

            sweeper = new Timer(_ => Sweep(), null, SWEEP_EVERY, SWEEP_EVERY);
            Program.Log.Info("Client collision found for {0} of {1} zones in {2} (loaded on first use, dropped after {3} min unused).",
                zoneList.Count(z => z.ZoneName != null && entryFor.ContainsKey(z.ZoneName)), zoneList.Count, Path.GetFullPath(Root),
                UNLOAD_AFTER.TotalMinutes);
            var missing = zoneList.Where(z => z.ZoneName == null || !entryFor.ContainsKey(z.ZoneName)).Select(z => z.ZoneName).OrderBy(n => n);
            Program.Log.Debug("Zones without client collision (navmesh heights only): {0}", string.Join(", ", missing));
        }

        public static bool Has(string zoneName) => zoneName != null && entryFor.ContainsKey(zoneName);

        /// <summary>Loads it on this thread if needed.</summary>
        public static CollisionWorld Get(string zoneName)
        {
            if (zoneName == null || !entryFor.TryGetValue(zoneName, out var e))
                return null;
            Touch(e);
            var w = e.World;
            if (w != null || e.Failed)
                return w;
            if (e.TryBeginLoad())
                Load(e);
            else
                while (e.Loading)
                    Thread.Sleep(10);
            return e.World;
        }

        /// <summary>Null while it loads in the background.</summary>
        public static CollisionWorld TryGet(Entry e)
        {
            Touch(e);
            var w = e.World;
            if (w == null && e.TryBeginLoad())
                Task.Run(() => Load(e));
            return w;
        }

        /// <summary>The highest hit over the zone and its neighbours. complete is false while any of them is
        /// still loading.</summary>
        public static ProbeResult Probe(string zoneName, float x, float z, float yFrom, NavInstance gates, out float y, out bool complete)
        {
            float top = yFrom + ClientProbe.RayUp;
            return RayDown(zoneName, x, z, top, top - (ClientProbe.RayDown + ClientProbe.RayUp), gates, out y, out complete);
        }

        /// <summary>The highest surface with bottom &lt; y &lt;= top.</summary>
        public static ProbeResult RayDown(string zoneName, float x, float z, float top, float bottom, NavInstance gates, out float y, out bool complete)
        {
            y = 0;
            complete = true;
            bool any = false, hit = false;
            float best = float.NegativeInfinity;
            if (zoneName != null && entryFor.TryGetValue(zoneName, out var own))
            {
                var w = TryGet(own);
                if (w != null)
                {
                    any = true;
                    var h = w.RayDown(x, z, top, bottom, w.StateFor(gates));
                    if (h.Hit)
                    {
                        hit = true;
                        best = h.Y;
                    }
                }
                else
                    complete = false;
            }
            if (zoneName != null && neighboursOf.TryGetValue(zoneName, out var neighbours))
            {
                foreach (var n in neighbours)
                {
                    if (!n.Covers(x, z))
                        continue;
                    var w = TryGet(n);
                    if (w == null)
                    {
                        complete = false;
                        continue;
                    }
                    any = true;
                    var h = w.RayDown(x, z, top, bottom);
                    if (h.Hit && h.Y > best)
                    {
                        hit = true;
                        best = h.Y;
                    }
                }
            }
            if (!any)
            {
                complete = false;
                return ProbeResult.NoCollision;
            }
            y = best;
            return hit ? ProbeResult.Hit : ProbeResult.Miss;
        }

        public static IEnumerable<CollisionWorld> Loaded => entries.Select(e => e.World).Where(w => w != null);

        private static void Touch(Entry e) => Volatile.Write(ref e.LastUsedMs, Environment.TickCount64);

        private static void Load(Entry e)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                var world = CollisionWorld.LoadZones(e.Name, e.Dir);
                e.World = world;
                e.Loads++;
                Program.Log.Info("Client collision {0}: {1} of {2} triangles probe-visible, {3} switchable groups, {4} ms, ~{5} MB.",
                    e.Name, world.TriangleCount, world.SourceTriangles, world.DynamicGroups.Count, sw.ElapsedMilliseconds,
                    world.MemoryBytes / (1024 * 1024));
            }
            catch (Exception ex)
            {
                e.Failed = true;
                Program.Log.Error("Client collision {0} failed to load: {1}", e.Name, ex.Message);
            }
            finally
            {
                e.EndLoad();
            }
        }

        private static void Sweep()
        {
            long now = Environment.TickCount64;
            foreach (var e in entries)
            {
                var w = e.World;
                if (w != null && now - Volatile.Read(ref e.LastUsedMs) > UNLOAD_AFTER.TotalMilliseconds)
                {
                    e.World = null;
                    Program.Log.Info("Client collision {0} unloaded after {1} min unused (~{2} MB).",
                        e.Name, UNLOAD_AFTER.TotalMinutes, w.MemoryBytes / (1024 * 1024));
                }
            }
        }
    }
}
