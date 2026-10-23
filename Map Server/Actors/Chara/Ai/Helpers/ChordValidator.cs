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
using System.Numerics;
using Meteor.Navigation;

namespace Meteor.Map.actors.chara.ai
{
    /// <summary>Unknown: no client collision is loaded there.</summary>
    enum GroundAnswer { Hit, Miss, Unknown }

    interface IChordWorld
    {
        /// <summary>Fraction of a → b that is walkable: 1 = clear, NaN when a is off the mesh.</summary>
        float Walk(Vector3 a, Vector3 b, float lateral);
        /// <summary>The highest surface with bottom &lt; y &lt;= top.</summary>
        GroundAnswer RayDown(float x, float z, float top, float bottom, out float y);
        /// <summary>under: the mesh is right under the point, not just near it.</summary>
        bool NavHeight(float x, float yNear, float z, out float y, out bool under);
        /// <summary>PositiveInfinity when further than max.</summary>
        float MeshDistance(Vector3 p, float max);
    }

    readonly struct ChordResult
    {
        public enum Why { Ok, TooShort, Wall, NoGround, FloorMismatch, Step, Overhang }

        public readonly Why Reason;
        /// <summary>How far along the line it failed.</summary>
        public readonly float S;
        public readonly float Length;

        public ChordResult(Why reason, float s, float length)
        {
            Reason = reason;
            S = s;
            Length = length;
        }

        public bool Ok => Reason == Why.Ok;
        public override string ToString() => Ok ? "ok" : $"{Reason} at {S:F2}/{Length:F2}";
    }

    /// <summary>
    /// Checks the straight line the client will walk an actor along after a 0x00CF. The client doesn't
    /// test walls and takes the height from its own ground probe each frame. So the navmesh must be
    /// clear along the line, the probe must find the same floor as the navmesh, and nothing may hang low
    /// enough over the floor for the probe to take it as ground and lift the actor into it.
    /// </summary>
    static class ChordValidator
    {
        /// <summary>How far to either side of the expected spot the client may really be.</summary>
        public const float MARGIN = 0.4f;
        public const float FLOOR_TOLERANCE = 0.3f;
        /// <summary>The client ignores a target this close to the last one.</summary>
        public const float MIN_MOVE = 0.1f;
        public const float SIDE_RADIUS = 0.5f, SIDE_SLOPE = 0.75f;
        private const float MARGIN_SLACK = 0.05f;
        /// <summary>A start right on the mesh edge reads as leaving the mesh at once, so the line is tried
        /// again from this far along. Shorter than the gap around any obstacle, so it can't skip one.</summary>
        public const float START_SLOP = 0.5f;
        private const float MIN_STEP = 0.1f, MAX_STEP = 0.5f;
        /// <summary>A larger gap between the probe's floor and the navmesh at the start means the actor is
        /// already on the wrong surface.</summary>
        public const float MAX_BIAS = 1.0f;

        /// <summary>The mesh stops 0.6 short of walls, so a line this far past its edge is still clear of them.
        /// Narrow tunnels need it: their routes hug the edge.</summary>
        public const float EDGE_TOLERANCE = 0.4f;
        private const float EDGE_STEP = 0.25f;

        public static float FloorTolerance = FLOOR_TOLERANCE;
        /// <summary>How far the probe's floor may sit above or below the navmesh before it counts as a
        /// different surface. The mesh is smoother than the ground, more so on slopes.</summary>
        public const float ABOVE_MESH = 0.45f, BELOW_MESH = 0.75f;

        /// <summary>c is where the client draws the actor, hover included. Samples one point per client frame.</summary>
        public static ChordResult Check(IChordWorld world, Vector3 c, Vector3 t, float speed, float hover, Vector3? lastTarget = null)
        {
            float dx = t.X - c.X, dz = t.Z - c.Z;
            float len = MathF.Sqrt(dx * dx + dz * dz);
            if (lastTarget.HasValue && XZ(t - lastTarget.Value) < MIN_MOVE)
                return new ChordResult(ChordResult.Why.TooShort, 0, len);
            if (len < 1e-4f)
                return new ChordResult(ChordResult.Why.Ok, 0, len);

            float f = Pass(world, c, t);
            var start = c;
            if (!float.IsNaN(f) && f < 1.0f && f * len < START_SLOP && len > 2 * START_SLOP)
            {
                var c1 = new Vector3(c.X + dx / len * START_SLOP, c.Y, c.Z + dz / len * START_SLOP);
                float f1 = Pass(world, c1, t);
                if (!float.IsNaN(f1))
                {
                    f = f1 >= 1.0f ? 1.0f : (START_SLOP + f1 * (len - START_SLOP)) / len;
                    start = c1;
                }
            }
            if (!float.IsNaN(f) && f < 1.0f)
                return new ChordResult(ChordResult.Why.Wall, f * len, len);

            float ux = dx / len, uz = dz / len;
            for (int side = -1; side <= 1; side += 2)
            {
                // The client may start up to MARGIN to either side, but no closer to a wall than it already is.
                var normal = new Vector3(-uz * side, 0, ux * side);
                float room = world.Walk(start, start + normal * MARGIN, 0);
                float m = float.IsNaN(room) ? MARGIN : Math.Max(0.0f, room * MARGIN - MARGIN_SLACK);
                if (m <= 0.0f)
                    continue;
                float fs = Pass(world, start + normal * m, t);
                if (!float.IsNaN(fs) && fs < 1.0f)
                {
                    float skipped = len - XZ(t - start);
                    return new ChordResult(ChordResult.Why.Wall, skipped + fs * (len - skipped), len);
                }
            }

            // The navmesh sits a little off the real floor. A change in that offset along the line means a different surface.
            float bias = 0;
            bool startGround = world.RayDown(c.X, c.Z, c.Y - hover + ClientProbe.RayUp, c.Y - hover - ClientProbe.RayDown, out float floor0) == GroundAnswer.Hit;
            if (startGround && world.NavHeight(c.X, floor0, c.Z, out float nav0, out bool under0) && under0 && MathF.Abs(floor0 - nav0) <= MAX_BIAS)
                bias = floor0 - nav0;
            float step = Math.Clamp(speed / 30.0f, MIN_STEP, MAX_STEP);
            int n = Math.Max(1, (int)MathF.Ceiling(len / step));
            float y = c.Y;
            float prevNav = float.NaN;
            float prevFloor = startGround ? floor0 : c.Y - hover;
            float clear = ClientProbe.RayUp + hover;
            for (int i = 1; i <= n; i++)
            {
                float s = len * i / n;
                float x = c.X + ux * s, z = c.Z + uz * s;
                var g = world.RayDown(x, z, y + ClientProbe.RayUp, y - ClientProbe.RayDown, out float floor);
                if (g == GroundAnswer.Unknown)
                    return new ChordResult(ChordResult.Why.Ok, 0, len);     // no collision loaded: the navmesh check is all there is
                if (g == GroundAnswer.Miss)
                    return new ChordResult(ChordResult.Why.NoGround, s, len);

                bool meshNear = world.NavHeight(x, floor, z, out float nav, out bool under);
                // Off the mesh, climbing onto something is wrong; dropping is left to the step check.
                if (!meshNear && floor - prevFloor > FloorTolerance)
                    return new ChordResult(ChordResult.Why.FloorMismatch, s, len);
                if (meshNear)
                {
                    // Beside the mesh, only rising above it is wrong; falling away is left to the step check.
                    float off = floor - nav - bias;
                    if (under ? off > ABOVE_MESH || off < -BELOW_MESH : floor - Math.Max(nav + bias, prevFloor) > FloorTolerance)
                        return new ChordResult(ChordResult.Why.FloorMismatch, s, len);
                    if (under)
                    {
                        if (!float.IsNaN(prevNav) && (nav - prevNav > clear || prevNav - nav >= 10.0f - hover))
                            return new ChordResult(ChordResult.Why.Step, s, len);
                        prevNav = nav;
                    }
                }

                if (world.RayDown(x, z, floor + clear, floor + Meteor.Navigation.CollisionWorld.HEADROOM_EPSILON, out _) == GroundAnswer.Hit)
                    return new ChordResult(ChordResult.Why.Overhang, s, len);
                for (int side = -1; side <= 1; side += 2)
                {
                    float sx = x - uz * SIDE_RADIUS * side, sz = z + ux * SIDE_RADIUS * side;
                    float ground = world.RayDown(sx, sz, floor + SIDE_SLOPE, floor - SIDE_SLOPE, out float sg) == GroundAnswer.Hit ? sg : floor;
                    if (world.RayDown(sx, sz, floor + clear, ground + Meteor.Navigation.CollisionWorld.HEADROOM_EPSILON, out float roof) == GroundAnswer.Hit
                        && roof > prevFloor + FloorTolerance
                        && world.RayDown(sx, sz, roof - Meteor.Navigation.CollisionWorld.HEADROOM_EPSILON, floor - SIDE_SLOPE, out _) == GroundAnswer.Hit)
                        // Only rock with floor under it is an overhang. A wall face has none, and walls are the
                        // navmesh check's job.
                        return new ChordResult(ChordResult.Why.Overhang, s, len);
                }
                y = floor + hover;
                prevFloor = floor;
            }
            return new ChordResult(ChordResult.Why.Ok, len, len);
        }

        private static readonly float[] RECOVER_RINGS = { 0.5f, 1.0f, 1.5f, 2.0f, 3.0f };
        private const int RECOVER_DIRECTIONS = 16;

        /// <summary>
        /// For an actor stuck in a notch of the mesh edge, where every line toward t fails at once. Finds a
        /// short hop nearby that passes, preferring one from which t can then be reached.
        /// </summary>
        public static bool Recover(IChordWorld world, Vector3 c, Vector3 t, float speed, float hover, out Vector3 hop)
        {
            hop = default;
            bool found = false, onward = false;
            float best = float.PositiveInfinity;
            float floor = c.Y - hover;
            foreach (float r in RECOVER_RINGS)
            {
                for (int i = 0; i < RECOVER_DIRECTIONS; i++)
                {
                    float a = MathF.PI * 2 * i / RECOVER_DIRECTIONS;
                    float x = c.X + MathF.Cos(a) * r, z = c.Z + MathF.Sin(a) * r;
                    if (world.MeshDistance(new Vector3(x, floor, z), 0.05f) > 0.05f
                        || !world.NavHeight(x, floor, z, out float y, out bool under) || !under)
                        continue;
                    var q = new Vector3(x, y, z);
                    if (!Check(world, c, q, speed, hover).Ok)
                        continue;
                    float rest = XZ(t - q);
                    bool on = Check(world, new Vector3(q.X, q.Y + hover, q.Z), t, speed, hover).Ok;
                    if (on && !onward || on == onward && rest < best)
                    {
                        hop = q;
                        best = rest;
                        onward = on;
                        found = true;
                    }
                }
                if (onward)
                    return true;
            }
            return found;
        }

        /// <summary>How far past an edge the mesh may carry on lower down (a ledge the client walks off).</summary>
        public const float LEDGE_GAP = 2.5f;
        /// <summary>Only drops count: a higher floor past an edge is a rock shelf.</summary>
        public const float LEDGE_DROP = 0.5f;
        private const int MAX_LEDGES = 2;
        private const float LEDGE_LANDING = 0.25f;

        /// <summary>Like IChordWorld.Walk, but a line that only grazes the mesh edge passes, and one that walks off a
        /// ledge carries on below (the ground checks judge the drop).</summary>
        private static float Pass(IChordWorld world, Vector3 a, Vector3 b)
        {
            float len = XZ(b - a);
            float f = world.Walk(a, b, 0);
            if (float.IsNaN(f) || f >= 1.0f || len < 1e-4f)
                return f;
            if (Grazes(world, a, b))
                return 1.0f;
            float ux = (b.X - a.X) / len, uz = (b.Z - a.Z) / len;
            var from = a;
            float at = 0.0f;
            for (int ledge = 0; ledge < MAX_LEDGES; ledge++)
            {
                float rest = len - at;
                float hit = at + f * rest;
                if (world.RayDown(from.X, from.Z, from.Y + ClientProbe.RayUp, from.Y - ClientProbe.RayDown, out float floorBefore) != GroundAnswer.Hit)
                    break;
                bool landed = false;
                for (float g = hit + EDGE_STEP; g <= hit + LEDGE_GAP && g < len; g += EDGE_STEP)
                {
                    float x = a.X + ux * g, z = a.Z + uz * g;
                    if (world.RayDown(x, z, floorBefore + ClientProbe.RayUp, floorBefore - ClientProbe.RayDown, out float floor) != GroundAnswer.Hit
                        || floorBefore - floor < LEDGE_DROP)
                        continue;
                    var p = new Vector3(x, floor + (a.Y - floorBefore), z);
                    if (world.MeshDistance(new Vector3(x, floor, z), LEDGE_LANDING) > LEDGE_LANDING)
                        continue;
                    from = p;
                    at = g;
                    landed = true;
                    break;
                }
                if (!landed)
                    break;
                f = world.Walk(from, b, 0);
                if (float.IsNaN(f) || f >= 1.0f || Grazes(world, from, b))
                    return 1.0f;
            }
            return (at + f * (len - at)) / len;
        }

        private static bool Grazes(IChordWorld world, Vector3 a, Vector3 b)
        {
            float len = XZ(b - a);
            int n = Math.Max(1, (int)MathF.Ceiling(len / EDGE_STEP));
            for (int i = 0; i <= n; i++)
            {
                if (world.MeshDistance(Vector3.Lerp(a, b, (float)i / n), EDGE_TOLERANCE) > EDGE_TOLERANCE)
                    return false;
            }
            return true;
        }

        private static float XZ(Vector3 v) => MathF.Sqrt(v.X * v.X + v.Z * v.Z);
    }
}
