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
using Meteor.Navigation;

namespace Meteor.Map.navigation
{
    // SmallIsland: the only ground is stray geometry no path leaves (old meshes).
    // VerticalMismatch: the nearest ground is far above or below the point (.nav worlds).
    enum SpawnCheck { Ok, SmallIsland, NotFound, VerticalMismatch }

    // An area's navigation, on an old MSET mesh (ZoneNavMesh, no gates) or a .nav world
    // (NavInstanceMesh).
    interface IZoneNav
    {
        string Name { get; }
        int TileCount { get; }
        int PolyCount { get; }

        // Null on an old mesh.
        NavInstance Instance { get; }

        bool TrySnapToGround(float x, float y, float z, out Vector3 snapped);
        SpawnCheck ProjectSpawn(float x, float y, float z, out Vector3 snapped);
        bool TryProject(Vector3 p, out Vector3 ground);
        bool TryFindPath(Vector3 start, Vector3 end, float stepSize, int maxPoints, float endRandomRadius, List<Vector3> output, out PathResult result);
        bool HasLineOfSight(Vector3 from, Vector3 to);
        bool TryClearance(Vector3 from, float[] dirX, float[] dirZ, float[] maxDist, float[] dist);
        bool TryRandomPointAround(Vector3 center, float radius, out Vector3 point);
        bool TryRandomPointInRing(Vector3 center, float minRadius, float maxRadius, out Vector3 point);
        bool IsReachable(Vector3 from, Vector3 to);
        string Describe(Vector3 pos);
    }
}
