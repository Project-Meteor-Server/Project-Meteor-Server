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
using DotRecast.Core;
using DotRecast.Core.Numerics;
using DotRecast.Detour;
using Meteor.Common;

namespace Meteor.Map.navigation
{
    /// <summary>An old MSET navmesh. Read-only once loaded; queries take a pooled context so any thread can run them.</summary>
    class ZoneNavMesh : IZoneNav
    {
        private static readonly RcVec3f NearExtents = new RcVec3f(2.0f, 4.0f, 2.0f);
        private static readonly RcVec3f WideExtents = new RcVec3f(8.0f, 32.0f, 8.0f);
        private const float COLUMN_HEIGHT = 2000.0f;
        // Some spawn points are only roughly placed (map spots are off by up to 100 yalms).
        private static readonly float[] SnapRadii = { 4.0f, 12.0f, 32.0f, 64.0f, 128.0f };
        // Smaller islands are stray geometry (rooftops, ledges, props).
        private const int MIN_MAIN_ISLAND_POLYS = 64;

        private static readonly IDtQueryFilter Filter = new DtQueryDefaultFilter();

        public readonly string FilePath;
        public readonly int IslandCount;
        private readonly int tileCount, polyCount;

        public string Name { get; }
        public int TileCount => tileCount;
        public int PolyCount => polyCount;
        public Meteor.Navigation.NavInstance Instance => null;

        private readonly DtNavMesh mesh;
        private readonly int[][] islandOf;      // [tile index][poly index] -> island id
        private readonly int[] islandSize;      // island id -> poly count
        private readonly ConcurrentBag<QueryContext> contexts = new ConcurrentBag<QueryContext>();

        private sealed class QueryContext
        {
            public DtNavMeshQuery Query;
            public long[] Polys = new long[512];
            public DtStraightPath[] Corners = new DtStraightPath[64];
            public RcRand Rand = new RcRand(Environment.TickCount);
        }

        public ZoneNavMesh(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
            mesh = MsetNavMeshLoader.Load(filePath, out tileCount, out polyCount);
            islandOf = BuildIslands(mesh, out islandSize);
            IslandCount = islandSize.Length;
            contexts.Add(NewContext()); // so the first query doesn't pay for one
        }

        #region Public queries (game coordinates: X east, Y up, Z south)

        /// <summary>Tries near the given height, then straight up and down, then further out.</summary>
        public bool TrySnapToGround(float x, float y, float z, out Vector3 snapped)
        {
            return Snap(x, y, z, out snapped, out _);
        }

        public SpawnCheck ProjectSpawn(float x, float y, float z, out Vector3 snapped)
        {
            if (!Snap(x, y, z, out snapped, out bool onMain))
                return SpawnCheck.NotFound;
            return onMain ? SpawnCheck.Ok : SpawnCheck.SmallIsland;
        }

        private bool Snap(float x, float y, float z, out Vector3 snapped, out bool onMain)
        {
            snapped = null;
            onMain = false;
            var ctx = Rent();
            try
            {
                var pos = new RcVec3f(x, y, z);

                // Y = 0 means the height is unknown. Searching near 0 found sea planes and basements under
                // the real ground, so search straight up and down first.
                if (y == 0.0f && TryColumn(x, z, out float h0, out onMain))
                {
                    snapped = new Vector3(x, h0, z);
                    return true;
                }

                if (FindNearest(ctx, pos, NearExtents, out long r, out var pt))
                {
                    snapped = new Vector3(pt.X, pt.Y, pt.Z);
                    onMain = IsMainIsland(r);
                    return true;
                }

                if (TryColumn(x, z, out float h, out onMain))
                {
                    snapped = new Vector3(x, h, z);
                    return true;
                }

                foreach (float radius in SnapRadii)
                {
                    if (FindNearest(ctx, pos, new RcVec3f(radius, COLUMN_HEIGHT, radius), out r, out pt))
                    {
                        snapped = new Vector3(pt.X, pt.Y, pt.Z);
                        onMain = IsMainIsland(r);
                        return true;
                    }
                }
                return false;
            }
            finally { Return(ctx); }
        }

        public bool TryProject(Vector3 p, out Vector3 ground)
        {
            ground = null;
            var ctx = Rent();
            try
            {
                if (!FindNearestAny(ctx, new RcVec3f(p.X, p.Y, p.Z), out _, out var pt))
                    return false;
                ground = new Vector3(pt.X, pt.Y, pt.Z);
                return true;
            }
            finally { Return(ctx); }
        }

        /// <summary>At most maxPoints long; callers plan again when they run out.</summary>
        public bool TryFindPath(Vector3 start, Vector3 end, float stepSize, int maxPoints, float endRandomRadius, List<Vector3> output)
        {
            return TryFindPath(start, end, stepSize, maxPoints, endRandomRadius, output, out _);
        }

        public bool TryFindPath(Vector3 start, Vector3 end, float stepSize, int maxPoints, float endRandomRadius, List<Vector3> output, out PathResult result)
        {
            long t0 = System.Diagnostics.Stopwatch.GetTimestamp();
            result = FindPath(start, end, stepSize, maxPoints, endRandomRadius, output);
            NavStats.Record(Name, result, System.Diagnostics.Stopwatch.GetTimestamp() - t0);
            return result == PathResult.Ok || result == PathResult.Partial;
        }

        private PathResult FindPath(Vector3 start, Vector3 end, float stepSize, int maxPoints, float endRandomRadius, List<Vector3> output)
        {
            output.Clear();
            var ctx = Rent();
            try
            {
                var startPos = new RcVec3f(start.X, start.Y, start.Z);
                var endPos = new RcVec3f(end.X, end.Y, end.Z);

                if (!FindNearestAny(ctx, startPos, out long startRef, out startPos))
                    return PathResult.StartOffMesh;
                if (!FindNearestAny(ctx, endPos, out long endRef, out endPos))
                    return PathResult.GoalOffMesh;

                if (IslandOf(startRef) != IslandOf(endRef))
                    return PathResult.NoRoute;

                if (endRandomRadius > 0.0f &&
                    ctx.Query.FindRandomPointAroundCircle(endRef, endPos, endRandomRadius, Filter, ctx.Rand, out long randRef, out var randPos).Succeeded())
                {
                    endRef = randRef;
                    endPos = randPos;
                }

                var status = ctx.Query.FindPath(startRef, endRef, startPos, endPos, Filter, ctx.Polys, out int polyCount, ctx.Polys.Length);
                if (status.Failed() || polyCount == 0)
                    return PathResult.Failed;

                bool partial = status.IsPartial();
                if (partial)
                    ctx.Query.ClosestPointOnPoly(ctx.Polys[polyCount - 1], endPos, out endPos, out _);

                status = ctx.Query.FindStraightPath(startPos, endPos, ctx.Polys, polyCount, ctx.Corners, out int cornerCount, ctx.Corners.Length, 0);
                if (status.Failed() || cornerCount == 0)
                    return PathResult.Failed;

                Densify(ctx, startPos, cornerCount, stepSize, maxPoints, output);
                if (output.Count == 0)
                    return PathResult.Failed;
                return partial ? PathResult.Partial : PathResult.Ok;
            }
            finally { Return(ctx); }
        }

        /// <summary>Points off the mesh count as visible.</summary>
        public bool HasLineOfSight(Vector3 from, Vector3 to)
        {
            var ctx = Rent();
            try
            {
                var startPos = new RcVec3f(from.X, from.Y, from.Z);
                if (!FindNearestAny(ctx, startPos, out long startRef, out startPos))
                    return true;

                var endPos = new RcVec3f(to.X, startPos.Y, to.Z);
                var status = ctx.Query.Raycast(startRef, startPos, endPos, Filter, out float t, out _, ctx.Polys, out _, ctx.Polys.Length);
                return status.Succeeded() && t >= 1.0f;
            }
            finally { Return(ctx); }
        }

        /// <summary>How far one can walk straight from 'from' in each direction, up to maxDist[i].
        /// False when 'from' is off the mesh.</summary>
        public bool TryClearance(Vector3 from, float[] dirX, float[] dirZ, float[] maxDist, float[] dist)
        {
            var ctx = Rent();
            try
            {
                var startPos = new RcVec3f(from.X, from.Y, from.Z);
                if (!FindNearest(ctx, startPos, NearExtents, out long startRef, out startPos))
                    return false;
                for (int i = 0; i < dirX.Length; i++)
                {
                    var endPos = new RcVec3f(startPos.X + dirX[i] * maxDist[i], startPos.Y, startPos.Z + dirZ[i] * maxDist[i]);
                    var status = ctx.Query.Raycast(startRef, startPos, endPos, Filter, out float t, out _, ctx.Polys, out _, ctx.Polys.Length);
                    dist[i] = status.Failed() ? 0.0f : (t >= 1.0f ? maxDist[i] : t * maxDist[i]);
                }
                return true;
            }
            finally { Return(ctx); }
        }

        public bool TryRandomPointAround(Vector3 center, float radius, out Vector3 point)
        {
            point = null;
            var ctx = Rent();
            try
            {
                var pos = new RcVec3f(center.X, center.Y, center.Z);
                if (!FindNearestAny(ctx, pos, out long centerRef, out pos))
                    return false;
                if (ctx.Query.FindRandomPointAroundCircle(centerRef, pos, radius, Filter, ctx.Rand, out _, out var p).Failed())
                    return false;
                point = new Vector3(p.X, p.Y, p.Z);
                return true;
            }
            finally { Return(ctx); }
        }

        /// <summary>Retries because Detour can return a point outside the radius on a large poly.</summary>
        public bool TryRandomPointInRing(Vector3 center, float minRadius, float maxRadius, out Vector3 point)
        {
            for (int attempt = 0; attempt < 8; attempt++)
            {
                if (!TryRandomPointAround(center, maxRadius, out point))
                    break;
                float d = Utils.XZDistance(center.X, center.Z, point.X, point.Z);
                if (d >= minRadius && d <= maxRadius)
                    return true;
            }
            point = null;
            return false;
        }

        public bool IsReachable(Vector3 from, Vector3 to)
        {
            var ctx = Rent();
            try
            {
                return FindNearestAny(ctx, new RcVec3f(from.X, from.Y, from.Z), out long a, out _) &&
                       FindNearestAny(ctx, new RcVec3f(to.X, to.Y, to.Z), out long b, out _) &&
                       IslandOf(a) == IslandOf(b);
            }
            finally { Return(ctx); }
        }

        public string Describe(Vector3 pos)
        {
            var ctx = Rent();
            try
            {
                if (!FindNearestAny(ctx, new RcVec3f(pos.X, pos.Y, pos.Z), out long r, out var p))
                    return $"{Name}: no navmesh within {WideExtents.X}y of ({pos.X:F1}, {pos.Y:F1}, {pos.Z:F1})";
                int island = IslandOf(r);
                return $"{Name}: ground ({p.X:F1}, {p.Y:F1}, {p.Z:F1}), {pos.Y - p.Y:F1}y above it, island {island} ({islandSize[island]} of {PolyCount} polys)";
            }
            finally { Return(ctx); }
        }

        #endregion

        #region Internals

        private QueryContext NewContext() => new QueryContext { Query = new DtNavMeshQuery(mesh) };

        private QueryContext Rent() => contexts.TryTake(out var ctx) ? ctx : NewContext();

        private void Return(QueryContext ctx) => contexts.Add(ctx);

        private static bool FindNearest(QueryContext ctx, RcVec3f pos, RcVec3f extents, out long polyRef, out RcVec3f point)
        {
            var status = ctx.Query.FindNearestPoly(pos, extents, Filter, out polyRef, out point, out _);
            return status.Succeeded() && polyRef != 0;
        }

        private static bool FindNearestAny(QueryContext ctx, RcVec3f pos, out long polyRef, out RcVec3f point)
        {
            return FindNearest(ctx, pos, NearExtents, out polyRef, out point) ||
                   FindNearest(ctx, pos, WideExtents, out polyRef, out point);
        }

        private bool IsMainIsland(long polyRef) => islandSize[IslandOf(polyRef)] >= MIN_MAIN_ISLAND_POLYS;

        private int IslandOf(long polyRef)
        {
            mesh.GetTileAndPolyByRefUnsafe(polyRef, out var tile, out var poly);
            return islandOf[tile.index][poly.index];
        }

        /// <summary>Prefers ground on a large island, then the highest.</summary>
        private bool TryColumn(float x, float z, out float height, out bool foundMain)
        {
            height = 0;
            bool found = false;
            foundMain = false;
            var probe = new RcVec3f(x, 0, z);

            mesh.CalcTileLoc(probe, out int tx, out int ty);
            var tiles = new DtMeshTile[32];
            int count = mesh.GetTilesAt(tx, ty, tiles, tiles.Length);

            for (int i = 0; i < count; i++)
            {
                var tile = tiles[i];
                for (int p = 0; p < tile.data.header.polyCount; p++)
                {
                    var poly = tile.data.polys[p];
                    if (poly.GetPolyType() != DtPolyTypes.DT_POLYTYPE_GROUND)
                        continue;
                    if (!mesh.GetPolyHeight(tile, poly, probe, out float h))
                        continue;

                    bool isMain = islandSize[islandOf[tile.index][p]] >= MIN_MAIN_ISLAND_POLYS;
                    if (!found || (isMain && !foundMain) || (isMain == foundMain && h > height))
                    {
                        height = h;
                        found = true;
                        foundMain = isMain;
                    }
                }
            }
            return found;
        }

        private void Densify(QueryContext ctx, RcVec3f startPos, int cornerCount, float stepSize, int maxPoints, List<Vector3> output)
        {
            var prev = startPos;
            float carried = 0.0f; // walked past the last point added

            for (int c = 0; c < cornerCount && output.Count < maxPoints; c++)
            {
                var corner = ctx.Corners[c].pos;
                float dx = corner.X - prev.X, dz = corner.Z - prev.Z;
                float segment = MathF.Sqrt(dx * dx + dz * dz);

                float d = stepSize - carried;
                while (d < segment && output.Count < maxPoints)
                {
                    float t = d / segment;
                    var p = new RcVec3f(prev.X + dx * t, prev.Y + (corner.Y - prev.Y) * t, prev.Z + dz * t);
                    if (FindNearest(ctx, p, NearExtents, out _, out var ground))
                        p = ground;
                    output.Add(new Vector3(p.X, p.Y, p.Z));
                    d += stepSize;
                }

                carried = segment - (d - stepSize);
                prev = corner;
            }

            if (output.Count < maxPoints)
            {
                var last = ctx.Corners[cornerCount - 1].pos;
                output.Add(new Vector3(last.X, last.Y, last.Z));
            }
        }

        private static int[][] BuildIslands(DtNavMesh mesh, out int[] sizes)
        {
            var ids = new int[mesh.GetMaxTiles()][];
            for (int t = 0; t < ids.Length; t++)
            {
                var tile = mesh.GetTile(t);
                if (tile?.data?.header != null)
                {
                    ids[t] = new int[tile.data.header.polyCount];
                    Array.Fill(ids[t], -1);
                }
            }

            var sizeList = new List<int>();
            var stack = new Stack<long>();

            for (int t = 0; t < ids.Length; t++)
            {
                if (ids[t] == null)
                    continue;
                var tile = mesh.GetTile(t);
                long baseRef = mesh.GetPolyRefBase(tile);

                for (int p = 0; p < ids[t].Length; p++)
                {
                    if (ids[t][p] != -1)
                        continue;

                    int island = sizeList.Count;
                    int size = 0;
                    ids[t][p] = island;
                    stack.Push(baseRef | (uint)p);

                    while (stack.Count > 0)
                    {
                        mesh.GetTileAndPolyByRefUnsafe(stack.Pop(), out var ct, out var cp);
                        size++;
                        for (int k = cp.firstLink; k != DtDetour.DT_NULL_LINK; k = ct.links[k].next)
                        {
                            long neighbour = ct.links[k].refs;
                            if (neighbour == 0)
                                continue;
                            mesh.GetTileAndPolyByRefUnsafe(neighbour, out var nt, out var np);
                            if (ids[nt.index][np.index] != -1)
                                continue;
                            ids[nt.index][np.index] = island;
                            stack.Push(neighbour);
                        }
                    }
                    sizeList.Add(size);
                }
            }

            sizes = sizeList.ToArray();
            return ids;
        }

        #endregion
    }
}
