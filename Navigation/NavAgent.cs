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
using System.Threading;
using DotRecast.Core.Numerics;
using DotRecast.Detour;

namespace Meteor.Navigation
{
    // What an actor may cross and how big it is. Use With() for variants.
    public sealed class NavAgent
    {
        private static int nextKey;

        public readonly string Name;
        public readonly int Key;
        public readonly float Radius;
        public readonly float Height;
        public readonly float Climb;
        public readonly NavFlags Include;
        public readonly NavFlags Exclude;
        // Areas not listed use the world's costs.
        public readonly IReadOnlyDictionary<int, float> AreaCosts;
        // Drops and climbs higher than this are closed. The client's ground probe only reaches 10 down.
        public readonly float MaxLinkRise;

        public NavAgent(string name, float radius, float height, float climb, NavFlags include, NavFlags exclude,
            IReadOnlyDictionary<int, float> areaCosts = null, float maxLinkRise = float.PositiveInfinity)
        {
            MaxLinkRise = maxLinkRise;
            Name = name;
            Key = Interlocked.Increment(ref nextKey);
            Radius = radius;
            Height = height;
            Climb = climb;
            Include = include;
            Exclude = exclude | NavFlags.Pruned;
            AreaCosts = areaCosts ?? new Dictionary<int, float>();
        }

        public NavAgent With(string name = null, float? radius = null, NavFlags? include = null, NavFlags? exclude = null,
            IReadOnlyDictionary<int, float> areaCosts = null)
        {
            return new NavAgent(name ?? Name, radius ?? Radius, Height, Climb, include ?? Include, exclude ?? Exclude, areaCosts ?? AreaCosts, MaxLinkRise);
        }

        public bool Passes(int flags) => (flags & (int)Include) != 0 && (flags & (int)Exclude) == 0;

        private const NavFlags Ground = NavFlags.Walk | NavFlags.Water | NavFlags.Gate | NavFlags.Link;

        // Ground where the client would draw the actor on something else, such as a cave ceiling.
        private const NavFlags ProbeUnsafe = NavFlags.NpcUnsafe;

        public static readonly NavAgent Mob = new NavAgent("Mob", 0.5f, 2.0f, 0.75f,
            Ground | NavFlags.Drop, NavFlags.PlayerOnly | NavFlags.Lift | ProbeUnsafe, maxLinkRise: CLIENT_LINK_RISE);

        public const float CLIENT_LINK_RISE = 9.5f;

        public static readonly NavAgent Escort = new NavAgent("Escort", 0.5f, 2.0f, 0.75f,
            Ground | NavFlags.Lift, NavFlags.PlayerOnly | NavFlags.Drop | ProbeUnsafe);

        public static readonly NavAgent Ally = new NavAgent("Ally", 0.5f, 2.0f, 0.75f,
            Ground | NavFlags.Lift | NavFlags.Drop, NavFlags.PlayerOnly | ProbeUnsafe, maxLinkRise: CLIENT_LINK_RISE);

        // Server-moved player-class actors: anywhere a player goes that other clients draw correctly.
        public static readonly NavAgent Bot = new NavAgent("Bot", 0.5f, 2.0f, 0.75f,
            Ground | NavFlags.Lift | NavFlags.Drop | NavFlags.PlayerOnly, ProbeUnsafe);

        public static readonly NavAgent Player = new NavAgent("Player", 0.5f, 2.0f, 0.75f,
            Ground | NavFlags.Lift | NavFlags.Drop | NavFlags.PlayerOnly, NavFlags.None);

        public override string ToString() => Name;
    }

    // Reused across queries, so it never allocates.
    internal sealed class NavFilter : IDtQueryFilter
    {
        private NavWorld world;
        private GateState gates;
        private float[] costs;
        private int include, exclude;
        private float maxLinkRise = float.PositiveInfinity;

        public void Set(NavWorld world, NavAgent agent, GateState gates)
        {
            this.world = world;
            this.gates = gates;
            costs = world.CostsFor(agent);
            include = (int)agent.Include;
            exclude = (int)agent.Exclude;
            maxLinkRise = agent.MaxLinkRise;
        }

        public void SetAny(NavWorld world, GateState gates)
        {
            this.world = world;
            this.gates = gates;
            costs = world.DefaultCosts;
            include = 0xFFFF;
            exclude = (int)NavFlags.Pruned;
            maxLinkRise = float.PositiveInfinity;
        }

        public bool PassFilter(long refs, DtMeshTile tile, DtPoly poly)
        {
            int f = poly.flags;
            if ((f & include) == 0 || (f & exclude) != 0)
                return false;
            if (maxLinkRise < float.PositiveInfinity && world.LinkRise(refs) > maxLinkRise)
                return false;
            if (gates != null)
            {
                int g = world.GateOfPoly(tile.index, poly.index);
                if (g >= 0 && !gates.IsOpen(g))
                    return false;
            }
            return true;
        }

        public float GetCost(RcVec3f pa, RcVec3f pb, long prevRef, DtMeshTile prevTile, DtPoly prevPoly,
            long curRef, DtMeshTile curTile, DtPoly curPoly, long nextRef, DtMeshTile nextTile, DtPoly nextPoly)
        {
            return RcVec3f.Distance(pa, pb) * costs[curPoly.GetArea()];
        }
    }
}
