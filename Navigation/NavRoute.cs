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

using System.Collections.Generic;
using System.Numerics;

namespace Meteor.Navigation
{
    /// <summary>
    /// A route planned one part at a time. Points hold the current part; while Complete is false, call
    /// Advance near its end. One per mover; not thread-safe.
    /// </summary>
    public sealed class NavRoute
    {
        public enum StretchKind { None, Straight, Local, Window }

        public readonly NavWorld World;
        public readonly NavInstance Instance;
        public readonly NavAgent Agent;
        public readonly RouteOpts Opts;
        public readonly Vector3 Target;

        public NavReason Reason { get; internal set; } = NavReason.Ok;
        public bool Complete { get; internal set; }
        public NavPoint Goal { get; internal set; }
        public Vector3 StretchStart { get; private set; }
        public StretchKind Stretch { get; internal set; }
        public int StretchCount { get; private set; }
        public int GateVersion { get; internal set; }

        internal List<int> NodePath;
        private readonly List<Vector3> points = new List<Vector3>();
        private readonly List<Vector3> corners = new List<Vector3>();

        public IReadOnlyList<Vector3> Points => points;
        public IReadOnlyList<Vector3> Corners => corners;
        public IReadOnlyList<int> GraphNodes => NodePath;

        public bool Ok => (Reason == NavReason.Ok || Reason == NavReason.Partial) && points.Count > 0;

        public bool Stale => Instance.GateVersion != GateVersion;

        internal NavRoute(NavWorld world, NavInstance instance, NavAgent agent, RouteOpts opts, Vector3 target, int gateVersion)
        {
            World = world;
            Instance = instance;
            Agent = agent;
            Opts = opts;
            Target = target;
            GateVersion = gateVersion;
        }

        /// <summary>False when there is nothing left to plan or planning failed (see Reason).</summary>
        public bool Advance(Vector3 pos)
        {
            if (Complete && !Stale)
                return false;
            World.AdvanceRoute(this, pos);
            return Ok;
        }

        public bool Advance(NavPoint start, Vector3 pos)
        {
            if (Complete && !Stale)
                return false;
            World.AdvanceRoute(this, pos, start);
            return Ok;
        }

        public float Length
        {
            get
            {
                float len = 0;
                var prev = StretchStart;
                foreach (var p in points)
                {
                    len += Vector3.Distance(prev, p);
                    prev = p;
                }
                return len;
            }
        }

        internal void BeginStretch(Vector3 start)
        {
            points.Clear();
            corners.Clear();
            StretchStart = start;
            StretchCount++;
            Complete = false;
            Reason = NavReason.Ok;
            Stretch = StretchKind.None;
        }

        internal void AddPoint(Vector3 p) => points.Add(p);

        internal void AddCorner(Vector3 p, bool link) => corners.Add(p);

        internal void Fail(NavReason reason)
        {
            points.Clear();
            corners.Clear();
            Reason = reason;
            Complete = true;
        }

        public override string ToString() =>
            $"{Reason} {(Complete ? "complete" : "partial-plan")} {Stretch} stretch {StretchCount}, {points.Count} points, {Length:F1}m";
    }
}
