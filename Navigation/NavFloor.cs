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
    // The floor a walker stands on, followed along the mesh tick to tick instead of looked up from its
    // position, so a walker in a tunnel never jumps to the ground above it. Looked up again only after
    // Reset, a warp, or a change of instance or agent. One per mover; not thread-safe.
    public sealed class NavFloor
    {
        // Actors walk closer to walls than the mesh edge.
        public const float SLACK = 1.0f;
        // A bigger move is a warp, not a step.
        public const float JUMP = 4.0f;
        // Further than this above or below, the actor was lifted or dropped. Below the agent's
        // height, so it never picks the floor over or under a tunnel.
        public const float VERTICAL = 2.0f;
        // About one step, so a slope counts but a tunnel's roof doesn't.
        public const float ABOVE = 0.6f;
        public const float BELOW = 6.0f;
        public const float SEARCH_RADIUS = 2.0f;

        private NavInstance instance;
        private NavAgent agent;
        private Vector3 meshAt;
        private Vector3 at;

        public long Poly { get; private set; }
        public int Acquired { get; private set; }

        // Call after a teleport.
        public void Reset() => Poly = 0;

        public NavPoint Track(NavInstance instance, NavAgent agent, Vector3 pos)
        {
            if (Poly != 0 && ReferenceEquals(instance, this.instance) && ReferenceEquals(agent, this.agent)
                && NavMath.XZDistance(pos, at) <= JUMP && Math.Abs(pos.Y - at.Y) <= JUMP)
            {
                var walked = instance.World.FollowFloor(Poly, meshAt, pos, agent, SLACK);
                if (walked.Ok && Math.Abs(walked.Position.Y - pos.Y) <= VERTICAL)
                    return Keep(walked, pos);
            }

            this.instance = instance;
            this.agent = agent;
            Acquired++;
            var found = instance.World.AcquireFloor(pos, agent, ABOVE, BELOW, SEARCH_RADIUS);
            if (found.Ok)
                return Keep(found, pos);
            Poly = 0;
            return found;
        }

        private NavPoint Keep(NavPoint floor, Vector3 pos)
        {
            Poly = floor.Poly;
            meshAt = floor.Position;
            at = pos;
            return floor;
        }

        public override string ToString() => Poly == 0 ? "no floor" : $"floor {Poly:X} ({Acquired} lookups)";
    }
}
