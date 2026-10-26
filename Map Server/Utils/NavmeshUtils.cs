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
using Meteor.Common;
using Meteor.Map.Actors;
using Meteor.Map.actors.area;
using Meteor.Map.actors.chara.npc;
using Meteor.Map.navigation;
using NavAgent = Meteor.Navigation.NavAgent;
using NavInstance = Meteor.Navigation.NavInstance;

namespace Meteor.Map.utils
{
    // Every call also works on zones without a navmesh, as straight lines.
    class NavmeshUtils
    {
        public static IZoneNav GetNavMesh(Area area)
        {
            if (area == null)
                return null;
            var instanceMesh = area.GetNavInstanceMesh();
            if (instanceMesh != null)
                return instanceMesh;
            if (area is PrivateArea privateArea)
                return privateArea.GetParentZone()?.navMesh;
            return (area as Zone)?.navMesh;
        }

        public static NavInstance GetNavInstance(Area area) => area?.GetNavInstance();

        // Which ground an actor may walk: monsters stay off lifts and player-only ground,
        // escorted npcs don't take drops, allies and pets go where the party goes.
        public static NavAgent AgentFor(Character chara)
        {
            if (chara is Player)
                return NavAgent.Player;
            if (chara is Ally || chara is Pet)
                return NavAgent.Ally;
            if (chara is BattleNpc)
                return NavAgent.Mob;
            if (chara is Npc)
                return NavAgent.Escort;
            return NavAgent.Mob;
        }

        public static bool CanSee(Area area, float x1, float y1, float z1, float x2, float y2, float z2)
        {
            var navMesh = GetNavMesh(area);
            return navMesh == null || navMesh.HasLineOfSight(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2));
        }

        // Without a navmesh, or off it, every direction is fully clear.
        public static void Clearance(Area area, Vector3 from, float[] dirX, float[] dirZ, float[] maxDist, float[] dist)
        {
            var navMesh = GetNavMesh(area);
            if (navMesh == null || !navMesh.TryClearance(from, dirX, dirZ, maxDist, dist))
                System.Array.Copy(maxDist, dist, maxDist.Length);
        }

        public static float ClearanceTowards(Area area, Vector3 from, Vector3 to)
        {
            float dx = to.X - from.X, dz = to.Z - from.Z;
            float len = (float)System.Math.Sqrt(dx * dx + dz * dz);
            if (len < 0.01f)
                return len;
            float[] dist = new float[1];
            Clearance(area, from, new[] { dx / len }, new[] { dz / len }, new[] { len }, dist);
            return dist[0];
        }

        // Returns the position unchanged when there is no ground near it.
        public static Vector3 SnapToGround(Area area, float x, float y, float z)
        {
            var navMesh = GetNavMesh(area);
            if (navMesh != null && navMesh.TrySnapToGround(x, y, z, out var snapped))
                return snapped;
            return new Vector3(x, y, z);
        }

        public static List<Vector3> GetPath(Area area, float x, float y, float z, float targetX, float targetY, float targetZ, float stepSize = 0.70f, int pathSize = 45, float polyRadius = 0.0f, bool skipToTarget = false)
        {
            return GetPath(area, new Vector3(x, y, z), new Vector3(targetX, targetY, targetZ), stepSize, pathSize, polyRadius, skipToTarget);
        }

        // Null when the end can't be reached or is the start. Without a navmesh, just the end.
        public static List<Vector3> GetPath(Area area, Vector3 startVec, Vector3 endVec, float stepSize = 0.70f, int pathSize = 45, float polyRadius = 0.0f, bool skipToTarget = false)
        {
            return GetPath(area, startVec, endVec, out _, stepSize, pathSize, polyRadius, skipToTarget);
        }

        public static List<Vector3> GetPath(Area area, Vector3 startVec, Vector3 endVec, out PathResult result, float stepSize = 0.70f, int pathSize = 45, float polyRadius = 0.0f, bool skipToTarget = false)
        {
            var navMesh = GetNavMesh(area);

            if (navMesh == null)
            {
                result = PathResult.NoMesh;
                NavStats.Record(null, result, 0);
                return new List<Vector3>() { endVec };
            }

            if (startVec.X == endVec.X && startVec.Y == endVec.Y && startVec.Z == endVec.Z && polyRadius == 0.0f)
            {
                result = PathResult.SamePoint;
                NavStats.Record(navMesh.Name, result, 0);
                return null;
            }

            var path = new List<Vector3>(pathSize);
            if (!navMesh.TryFindPath(startVec, endVec, stepSize, pathSize, polyRadius, path, out result))
                return null;

            if (skipToTarget)
                return new List<Vector3>() { path[path.Count - 1] };
            return path;
        }
    }
}
