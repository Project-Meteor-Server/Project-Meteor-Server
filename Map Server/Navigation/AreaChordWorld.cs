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
using Meteor.Map.actors.area;
using Meteor.Map.actors.chara.ai;
using Meteor.Map.Actors;
using Meteor.Map.utils;
using Meteor.Navigation;
using Num = System.Numerics;

namespace Meteor.Map.navigation
{
    // What ChordValidator checks a line against: the area's navmesh and the client collision
    // of its zone and the zones next to it.
    sealed class AreaChordWorld : IChordWorld
    {
        private readonly Area area;
        private readonly NavInstance nav;
        private readonly NavAgent agent;
        private readonly string zone;
        private const float NAV_HEIGHT_SNAP = 0.1f;

        public AreaChordWorld(Area area, NavAgent agent)
        {
            this.area = area;
            this.agent = agent;
            nav = area?.GetNavInstance();
            zone = area is PrivateArea pa ? pa.GetParentZone()?.ZoneName : area?.ZoneName;
        }

        public float Walk(Num.Vector3 a, Num.Vector3 b, float lateral)
        {
            if (lateral != 0)
            {
                float dx = b.X - a.X, dz = b.Z - a.Z;
                float len = MathF.Sqrt(dx * dx + dz * dz);
                if (len < 1e-4f)
                    return 1;
                var off = new Num.Vector3(-dz / len * lateral, 0, dx / len * lateral);
                a += off;
                b += off;
            }
            if (nav != null)
            {
                var hit = nav.Walk(a, b, agent);
                switch (hit.Reason)
                {
                    case NavReason.Ok: return hit.T >= 1.0f ? 1.0f : hit.T;
                    case NavReason.VerticalMismatch: return 1.0f;
                    default: return float.NaN;
                }
            }
            float total = MathF.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Z - a.Z) * (b.Z - a.Z));
            if (total < 0.01f)
                return 1;
            float clear = NavmeshUtils.ClearanceTowards(area, NavInstanceMesh.M(a), NavInstanceMesh.M(b));
            return Math.Min(1.0f, clear / total);
        }

        public GroundAnswer RayDown(float x, float z, float top, float bottom, out float y)
        {
            var r = CollisionManager.RayDown(zone, x, z, top, bottom, nav, out y, out _);
            return r == CollisionManager.ProbeResult.Hit ? GroundAnswer.Hit : r == CollisionManager.ProbeResult.Miss ? GroundAnswer.Miss : GroundAnswer.Unknown;
        }

        public float MeshDistance(Num.Vector3 p, float max)
        {
            if (nav == null)
                return float.PositiveInfinity;
            var q = nav.Project(p, agent, 4.0f, max + 0.05f);
            if (!q.Ok)
                return float.PositiveInfinity;
            float d = MathF.Sqrt((q.Position.X - p.X) * (q.Position.X - p.X) + (q.Position.Z - p.Z) * (q.Position.Z - p.Z));
            return d > max ? float.PositiveInfinity : d;
        }

        public bool NavHeight(float x, float yNear, float z, out float y, out bool under)
        {
            y = 0;
            under = false;
            if (nav == null)
                return false;
            var p = nav.Project(new Num.Vector3(x, yNear, z), agent, 2.0f, 0.25f);
            if (!p.Ok)
                return false;
            y = p.Position.Y;
            under = (p.Position.X - x) * (p.Position.X - x) + (p.Position.Z - z) * (p.Position.Z - z) <= NAV_HEIGHT_SNAP * NAV_HEIGHT_SNAP;
            return true;
        }
    }
}
