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
using System.Linq;
using Meteor.Common;
using Meteor.Navigation;
using Num = System.Numerics;

namespace Meteor.Map.navigation
{
    /// <summary>IZoneNav on an area's .nav world instance, so game code works the same on both mesh kinds.</summary>
    class NavInstanceMesh : IZoneNav
    {
        private const float SPAWN_VERTICAL_SLACK = 4.0f;

        private readonly NavInstance instance;

        public NavInstanceMesh(NavInstance instance)
        {
            this.instance = instance;
        }

        public NavInstance Instance => instance;
        public string Name => instance.World.Name;
        public int TileCount => instance.World.TileCount;
        public int PolyCount => instance.World.PolyCount;

        public static Num.Vector3 N(Vector3 v) => new Num.Vector3(v.X, v.Y, v.Z);
        public static Vector3 M(Num.Vector3 v) => new Vector3(v.X, v.Y, v.Z);

        public static PathResult ToPathResult(NavReason r)
        {
            switch (r)
            {
                case NavReason.Ok: return PathResult.Ok;
                case NavReason.Partial: return PathResult.Partial;
                case NavReason.StartOffMesh: return PathResult.StartOffMesh;
                case NavReason.GoalOffMesh: return PathResult.GoalOffMesh;
                case NavReason.GateClosed: return PathResult.GateClosed;
                case NavReason.VerticalMismatch: return PathResult.VerticalMismatch;
                default: return PathResult.NoRoute;
            }
        }

        // The mesh runs a little past the zone line into the next zone; spawns and roam points stay on this side.
        private static readonly NavAgent OwnGround = NavAgent.Player.With(exclude: NavFlags.Seam);
        private static readonly NavAgent MobOwnGround = NavAgent.Mob.With(exclude: NavAgent.Mob.Exclude | NavFlags.Seam);

        public bool TrySnapToGround(float x, float y, float z, out Vector3 snapped)
        {
            var p = instance.Snap(new Num.Vector3(x, y, z), OwnGround);
            snapped = p.Ok ? M(p.Position) : null;
            return p.Ok;
        }

        /// <summary>A spawn on pruned ground (rooftops, props) snaps far below or beside it: VerticalMismatch.</summary>
        public SpawnCheck ProjectSpawn(float x, float y, float z, out Vector3 snapped)
        {
            var p = instance.Snap(new Num.Vector3(x, y, z), OwnGround);
            if (!p.Ok)
            {
                snapped = null;
                return SpawnCheck.NotFound;
            }
            snapped = M(p.Position);
            if (y != 0.0f && Math.Abs(p.Position.Y - y) > SPAWN_VERTICAL_SLACK)
                return SpawnCheck.VerticalMismatch;
            return SpawnCheck.Ok;
        }

        public bool TryProject(Vector3 p, out Vector3 ground)
        {
            var g = instance.Project(N(p), NavAgent.Player, 32.0f, 8.0f);
            ground = g.Ok ? M(g.Position) : null;
            return g.Ok;
        }

        public bool TryFindPath(Vector3 start, Vector3 end, float stepSize, int maxPoints, float endRandomRadius, List<Vector3> output, out PathResult result)
        {
            output.Clear();
            long t0 = System.Diagnostics.Stopwatch.GetTimestamp();
            var route = instance.PlanRoute(N(start), N(end), NavAgent.Mob, new RouteOpts { SampleStep = stepSize, EndRandomRadius = endRandomRadius });
            result = ToPathResult(route.Reason);
            NavStats.Record(Name, result, System.Diagnostics.Stopwatch.GetTimestamp() - t0);
            foreach (var p in route.Points.Take(maxPoints))
                output.Add(M(p));
            return route.Ok;
        }

        public bool HasLineOfSight(Vector3 from, Vector3 to) => instance.CanSee(N(from), N(to));

        /// <summary>False when 'from' is off the mesh. Players can stand off it (closer to walls than the
        /// mesh edge, on props), and measured from the nearest edge every ray would read as blocked.</summary>
        public bool TryClearance(Vector3 from, float[] dirX, float[] dirZ, float[] maxDist, float[] dist)
        {
            var ground = instance.Project(N(from), NavAgent.Player, 2.0f, 2.0f);
            if (!ground.Ok || !IsOnMesh(from.X, from.Z, ground.Position, instance.World.WalkStartSlack))
                return false;
            var f = ground.Position;
            for (int i = 0; i < dirX.Length; i++)
            {
                if (!float.IsFinite(dirX[i]) || !float.IsFinite(dirZ[i]) || !float.IsFinite(maxDist[i]))
                {
                    dist[i] = float.IsFinite(maxDist[i]) ? maxDist[i] : 0.0f;
                    continue;
                }
                var hit = instance.Walk(f, new Num.Vector3(f.X + dirX[i] * maxDist[i], f.Y, f.Z + dirZ[i] * maxDist[i]), NavAgent.Player);
                // Only Ok means a wall; another floor or a failed query doesn't.
                dist[i] = hit.Reason == NavReason.Ok ? Math.Min(1.0f, Math.Max(0.0f, hit.T)) * maxDist[i] : maxDist[i];
            }
            return true;
        }

        private static bool IsOnMesh(float x, float z, Num.Vector3 ground, float slack) =>
            Utils.XZDistance(x, z, ground.X, ground.Z) <= slack;

        public bool TryRandomPointAround(Vector3 center, float radius, out Vector3 point) =>
            TryRandomPointInRing(center, 0.0f, radius, out point);

        public bool TryRandomPointInRing(Vector3 center, float minRadius, float maxRadius, out Vector3 point)
        {
            var p = instance.RandomPoint(N(center), minRadius, maxRadius, MobOwnGround);
            point = p.Ok ? M(p.Position) : null;
            return p.Ok;
        }

        public bool IsReachable(Vector3 from, Vector3 to) => instance.Reachable(N(from), N(to), NavAgent.Mob).Reachable;

        public string Describe(Vector3 pos)
        {
            var world = instance.World;
            var p = instance.Project(N(pos), NavAgent.Player, 32.0f, 8.0f);
            if (!p.Ok)
                return $"{Name} [{instance.Name}]: no ground within 32y of ({pos.X:F1}, {pos.Y:F1}, {pos.Z:F1}) ({p.Reason})";
            int gate = world.GateOfPoly(p.Poly);
            string gateText = gate < 0 ? "no gate" : $"gate {world.Gates[gate].Id} ({world.Gates[gate].Kind}, {(instance.IsGateOpen(gate) ? "open" : "closed")})";
            return $"{Name} [{instance.Name}]: ground ({p.Position.X:F1}, {p.Position.Y:F1}, {p.Position.Z:F1}), {pos.Y - p.Position.Y:F1}y above it, " +
                   $"area {world.PolyArea(p.Poly)} flags 0x{world.PolyFlags(p.Poly):X}, {gateText}, node {world.NodeOf(p.Poly)}, island {world.IslandOf(p.Poly)}, gate version {instance.GateVersion}";
        }

        public string DescribeGates()
        {
            var world = instance.World;
            if (world.Gates.Count == 0)
                return $"{Name} [{instance.Name}] has no gates.";
            return $"{Name} [{instance.Name}] gates (version {instance.GateVersion}): " + string.Join("; ", world.Gates.Select(g =>
                $"{g.Id} {g.Kind} {(instance.IsGateOpen(g.Index) ? "open" : "closed")} (default {g.Default}" +
                (g.Driver.Count > 0 ? ", " + string.Join(", ", g.Driver.Select(kv => kv.Key + "=" + kv.Value)) : "") + ")"));
        }
    }
}
