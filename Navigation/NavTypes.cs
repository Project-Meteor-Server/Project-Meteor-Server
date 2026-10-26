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

namespace Meteor.Navigation
{
    public static class NavArea
    {
        public const int NULL = 0;
        public const int GROUND = 1;
        public const int WATER = 2;
        public const int PLAYER_ONLY = 3;
        public const int GATE_0 = 4;
        public const int GATE_3 = 7;
        public const int LINK = 8;
        public const int LIFT = 9;
        public const int DROP = 10;
        // Players may walk it, but the client would draw server-moved actors wrong there.
        public const int NPC_UNSAFE = 11;
        public const int MAX = 64;
    }

    [Flags]
    public enum NavFlags : ushort
    {
        None = 0,
        Walk = 0x0001,
        Water = 0x0002,
        PlayerOnly = 0x0004,
        Gate = 0x0008,
        Lift = 0x0010,
        Drop = 0x0020,
        Link = 0x0040,
        // Past the zone line: walkable, but it belongs to the next zone.
        Seam = 0x0080,
        // The client would draw a server-moved actor somewhere else here.
        NpcUnsafe = 0x0100,
        Pruned = 0x8000,
    }

    public enum NavReason
    {
        Ok,
        Partial,            // ends at the closest reachable point
        StartOffMesh,
        GoalOffMesh,
        NoRoute,            // both ends on the mesh, but not connected for this agent
        GateClosed,         // a route exists with every gate open
        VerticalMismatch,   // ground there, but too far above or below
    }

    public readonly struct NavPoint
    {
        public readonly Vector3 Position;
        public readonly long Poly;
        public readonly NavReason Reason;

        public NavPoint(Vector3 position, long poly, NavReason reason)
        {
            Position = position;
            Poly = poly;
            Reason = reason;
        }

        public bool Ok => Reason == NavReason.Ok;

        public static NavPoint Fail(NavReason reason) => new NavPoint(default, 0, reason);

        public override string ToString() => Ok ? $"({Position.X:F2}, {Position.Y:F2}, {Position.Z:F2})" : Reason.ToString();
    }

    public readonly struct RayHit
    {
        public readonly NavReason Reason;
        // 1 = clear.
        public readonly float T;
        public readonly Vector3 Point;
        public readonly Vector3 Normal;

        public RayHit(NavReason reason, float t, Vector3 point, Vector3 normal)
        {
            Reason = reason;
            T = t;
            Point = point;
            Normal = normal;
        }

        public bool Clear => Reason == NavReason.Ok && T >= 1.0f;
    }

    public readonly struct Reach
    {
        public readonly NavReason Reason;
        public Reach(NavReason reason) { Reason = reason; }
        public bool Reachable => Reason == NavReason.Ok;
        public override string ToString() => Reason.ToString();
    }

    public sealed class RouteOpts
    {
        public static readonly RouteOpts Default = new RouteOpts();

        public float MaxVertical = 4.0f;
        public float SearchRadius = 4.0f;
        // Trips up to this long try a straight line first.
        public float StraightMaxDistance = 24.0f;
        // 0 = corners only.
        public float SampleStep = 2.0f;
        // How many graph cells around the start one part of the route covers.
        public int WindowCells = 2;
        // Spreads out a pack of chasers.
        public float EndRandomRadius = 0.0f;
    }

    internal static class NavMath
    {
        public static float XZDistance(Vector3 a, Vector3 b)
        {
            float dx = a.X - b.X, dz = a.Z - b.Z;
            return MathF.Sqrt(dx * dx + dz * dz);
        }
    }
}
