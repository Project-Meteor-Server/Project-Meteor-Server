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
using System.Linq;
using System.Threading;

namespace Meteor.Map.navigation
{
    enum PathResult
    {
        Ok,
        Partial,        // ends at the closest reachable point
        NoMesh,         // the area has no navmesh: straight line
        SamePoint,
        StartOffMesh,
        GoalOffMesh,
        NoRoute,        // both ends on the mesh, on different islands
        Failed,         // Detour failed or returned nothing
        GateClosed,     // a route exists with every gate open (.nav worlds)
        VerticalMismatch, // ground at that XZ, but far above/below the goal (.nav worlds)
    }

    // Path query counts and times per navmesh file. Times are Stopwatch ticks.
    static class NavStats
    {
        public const string NO_MESH = "(no navmesh)";
        private static readonly int ResultCount = Enum.GetValues(typeof(PathResult)).Length;

        public sealed class Counters
        {
            public long Calls;
            public long Ticks;
            public long MaxTicks;
            public readonly long[] Results = new long[ResultCount];
        }

        private static readonly ConcurrentDictionary<string, Counters> byMesh = new ConcurrentDictionary<string, Counters>(StringComparer.OrdinalIgnoreCase);
        private static DateTime since = DateTime.Now;

        public static void Record(string mesh, PathResult result, long elapsedTicks)
        {
            var c = byMesh.GetOrAdd(mesh ?? NO_MESH, _ => new Counters());
            Interlocked.Increment(ref c.Calls);
            Interlocked.Add(ref c.Ticks, elapsedTicks);
            Interlocked.Increment(ref c.Results[(int)result]);
            long max;
            while (elapsedTicks > (max = Interlocked.Read(ref c.MaxTicks)) && Interlocked.CompareExchange(ref c.MaxTicks, elapsedTicks, max) != max) { }
        }

        public static double ToMicroseconds(long ticks) => ticks * 1_000_000.0 / Stopwatch.Frequency;

        public static void Reset()
        {
            byMesh.Clear();
            since = DateTime.Now;
        }

        public static DateTime Since => since;

        public static IEnumerable<KeyValuePair<string, Counters>> Meshes => byMesh.OrderBy(k => k.Key);

        public static bool IsFailure(PathResult r) => r != PathResult.Ok && r != PathResult.Partial && r != PathResult.NoMesh && r != PathResult.SamePoint;
    }
}
