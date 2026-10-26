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
using Meteor.Common;

namespace Meteor.Map.actors.chara.ai
{
    // A route is a list of ground points starting where the walk started; the index is the
    // point being walked toward.
    static class MoverMath
    {
        public static float XZDistance(Vector3 a, Vector3 b)
        {
            float dx = a.X - b.X, dz = a.Z - b.Z;
            return (float)Math.Sqrt(dx * dx + dz * dz);
        }

        public static float Budget(float speed, double dt)
        {
            return speed <= 0.0f || dt <= 0.0 ? 0.0f : (float)(speed * dt);
        }

        // Walks budget yalms along the route. Height comes from the route so the actor follows
        // slopes. index ends on the next point ahead (points.Count when done).
        public static Vector3 Advance(Vector3 pos, IList<Vector3> points, ref int index, float budget, out float travelled)
        {
            travelled = 0.0f;
            Vector3 cur = new Vector3(pos.X, pos.Y, pos.Z);
            while (index < points.Count && budget > 0.0f)
            {
                Vector3 end = points[index];
                Vector3 start = index > 0 ? points[index - 1] : pos;
                float remaining = XZDistance(cur, end);
                if (remaining <= budget)
                {
                    cur = new Vector3(end.X, end.Y, end.Z);
                    budget -= remaining;
                    travelled += remaining;
                    index++;
                    continue;
                }

                float segment = XZDistance(start, end);
                float k = budget / remaining;
                float x = cur.X + (end.X - cur.X) * k;
                float z = cur.Z + (end.Z - cur.Z) * k;
                float t = segment > 0.0001f ? 1.0f - (remaining - budget) / segment : 1.0f;
                t = Math.Max(0.0f, Math.Min(1.0f, t));
                cur = new Vector3(x, start.Y + (end.Y - start.Y) * t, z);
                travelled += budget;
                budget = 0.0f;
            }
            return cur;
        }

        public static float PathLength(IList<Vector3> points, int from = 0)
        {
            float length = 0.0f;
            for (int i = Math.Max(1, from + 1); i < points.Count; i++)
                length += XZDistance(points[i - 1], points[i]);
            return length;
        }

        // Measured along the route, so a chase stops in range without cutting through walls.
        // Never removes the start.
        public static void TrimEnd(List<Vector3> points, float cut)
        {
            while (cut > 0.0f && points.Count > 1)
            {
                Vector3 last = points[points.Count - 1];
                Vector3 prev = points[points.Count - 2];
                float segment = XZDistance(prev, last);
                if (segment <= cut)
                {
                    cut -= segment;
                    points.RemoveAt(points.Count - 1);
                    continue;
                }
                float t = (segment - cut) / segment;
                points[points.Count - 1] = new Vector3(prev.X + (last.X - prev.X) * t, prev.Y + (last.Y - prev.Y) * t, prev.Z + (last.Z - prev.Z) * t);
                cut = 0.0f;
            }
        }

        // Spreads points evenly by area across the ring.
        public static double RingRadius(double u, float min, float max)
        {
            if (max <= min)
                return min;
            return Math.Sqrt(u * (max * (double)max - min * (double)min) + min * (double)min);
        }
    }
}
