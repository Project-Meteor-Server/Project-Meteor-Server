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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Meteor.Map.actors.area;
using Meteor.Navigation;

namespace Meteor.Map.navigation
{
    /// <summary>Zones listed in manifest.json get a .nav world; the rest fall back to "zoneName.bin".
    /// Zones that share a file share one copy.</summary>
    static class NavMeshManager
    {
        public const string MANIFEST = "manifest.json";

        private static readonly ConcurrentDictionary<string, ZoneNavMesh> byFile = new ConcurrentDictionary<string, ZoneNavMesh>(StringComparer.OrdinalIgnoreCase);
        private static readonly ConcurrentDictionary<string, NavWorld> worlds = new ConcurrentDictionary<string, NavWorld>(StringComparer.OrdinalIgnoreCase);

        public static ZoneNavMesh Get(string fileName) => byFile.TryGetValue(fileName, out var mesh) ? mesh : null;

        public static IEnumerable<NavWorld> Worlds => worlds.Values;

        public static void LoadForZones(IEnumerable<Zone> zones)
        {
            if (!ConfigConstants.NAVMESH_ENABLED)
            {
                Program.Log.Info("Navmesh loading is disabled in map_config.ini.");
                return;
            }

            string dir = ConfigConstants.NAVMESH_PATH;
            if (!Directory.Exists(dir))
            {
                Program.Log.Error("Navmesh folder \"{0}\" not found; mobs will walk in straight lines.", Path.GetFullPath(dir));
                return;
            }

            var zoneList = zones.ToList();
            var navFor = LoadNavWorlds(dir, zoneList);

            var fileFor = new Dictionary<Zone, string>();
            foreach (var zone in zoneList)
            {
                if (navFor.ContainsKey(zone))
                    continue;
                string file = ResolveFile(dir, zone.ZoneName);
                if (file != null)
                    fileFor[zone] = file;
            }

            var sw = Stopwatch.StartNew();
            long memBefore = GC.GetTotalMemory(false);
            var failures = new ConcurrentBag<string>();

            Parallel.ForEach(fileFor.Values.Distinct(StringComparer.OrdinalIgnoreCase), file =>
            {
                string name = Path.GetFileNameWithoutExtension(file);
                try
                {
                    byFile[name] = new ZoneNavMesh(name, file);
                }
                catch (Exception e)
                {
                    failures.Add($"{name}: {e.Message}");
                }
            });

            foreach (var pair in fileFor)
                pair.Key.navMesh = Get(Path.GetFileNameWithoutExtension(pair.Value));

            sw.Stop();
            foreach (var failure in failures)
                Program.Log.Error("Navmesh failed to load: {0}", failure);

            var loaded = byFile.Values.ToList();
            Program.Log.Info("Loaded {0} old navmeshes ({1} tiles, {2} polys) for {3} of {4} zones in {5} ms, ~{6} MB.",
                loaded.Count, loaded.Sum(m => m.TileCount), loaded.Sum(m => (long)m.PolyCount),
                zoneList.Count(z => z.navMesh != null), zoneList.Count, sw.ElapsedMilliseconds,
                Math.Max(0, GC.GetTotalMemory(false) - memBefore) / (1024 * 1024));

            var missing = zoneList.Where(z => z.navMesh == null && z.navWorld == null).Select(z => z.ZoneName).OrderBy(n => n);
            Program.Log.Debug("Zones without a navmesh: {0}", string.Join(", ", missing));
        }

        private static Dictionary<Zone, NavWorld> LoadNavWorlds(string dir, List<Zone> zoneList)
        {
            var result = new Dictionary<Zone, NavWorld>();
            string manifestPath = Path.Combine(dir, MANIFEST);
            if (!File.Exists(manifestPath))
                return result;

            NavManifest manifest;
            try
            {
                if (NavManifest.PeekVersion(manifestPath) < 2)
                    return result; // v1 lists .bin meshes
                manifest = NavManifest.Load(manifestPath);
            }
            catch (Exception e)
            {
                Program.Log.Error("Navmesh manifest {0} unreadable, using old navmeshes only: {1}", Path.GetFullPath(manifestPath), e.Message);
                return result;
            }

            var sw = Stopwatch.StartNew();
            var fileFor = new Dictionary<Zone, string>();
            foreach (var zone in zoneList)
            {
                string file = string.IsNullOrEmpty(zone.ZoneName) ? null : manifest.ResolveFile(zone.ZoneName);
                if (file == null)
                    continue;
                if (!File.Exists(file))
                {
                    Program.Log.Warn("Navmesh manifest lists {0} for {1}, but the file is missing.", file, zone.ZoneName);
                    continue;
                }
                if (manifest.Resolve(zone.ZoneName).Valid == false)
                    Program.Log.Warn("Navmesh {0} for {1} failed navgen validate; loading it anyway.", Path.GetFileName(file), zone.ZoneName);
                fileFor[zone] = Path.GetFullPath(file);
            }

            var failures = new ConcurrentBag<string>();
            Parallel.ForEach(fileFor.Values.Distinct(StringComparer.OrdinalIgnoreCase), file =>
            {
                try
                {
                    var world = NavWorld.Load(file, Path.GetFileNameWithoutExtension(file));
                    worlds[file] = world;
                    foreach (var w in world.Warnings)
                        Program.Log.Warn("Navmesh {0}: {1}", world.Name, w);
                }
                catch (Exception e)
                {
                    failures.Add($"{Path.GetFileName(file)}: {e.Message}");
                }
            });
            foreach (var failure in failures)
                Program.Log.Error("Navmesh failed to load: {0}", failure);

            foreach (var pair in fileFor)
                if (worlds.TryGetValue(pair.Value, out var world))
                {
                    pair.Key.navWorld = world;
                    result[pair.Key] = world;
                }

            var loaded = worlds.Values.ToList();
            Program.Log.Info("Loaded {0} .nav worlds ({1} tiles, {2} polys, {3} gates) for {4} zones in {5} ms.",
                loaded.Count, loaded.Sum(w => w.TileCount), loaded.Sum(w => (long)w.PolyCount), loaded.Sum(w => w.Gates.Count),
                result.Count, sw.ElapsedMilliseconds);
            return result;
        }

        /// <summary>Variant zones ("sea0Field01a") use the base zone's file.</summary>
        private static string ResolveFile(string dir, string zoneName)
        {
            if (string.IsNullOrEmpty(zoneName))
                return null;

            string path = Path.Combine(dir, zoneName + ".bin");
            if (File.Exists(path))
                return path;

            if (zoneName.EndsWith("a", StringComparison.Ordinal))
            {
                path = Path.Combine(dir, zoneName.Substring(0, zoneName.Length - 1) + ".bin");
                if (File.Exists(path))
                    return path;
            }
            return null;
        }
    }
}
