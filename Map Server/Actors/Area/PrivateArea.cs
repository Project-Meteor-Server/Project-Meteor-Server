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


using Meteor.Common;
using Meteor.Map.Actors;
using Meteor.Map.lua;
using Meteor.Map.packets.send.actor;
using System.Collections.Generic;

namespace Meteor.Map.actors.area
{
    class PrivateArea : Area    
    {
        private readonly Zone ParentZone;
        private readonly string PrivateAreaName;
        private readonly int PrivateAreaType;
        private readonly bool CanExitArea;

        public PrivateArea(Zone parent, string classPath, string privateAreaName, int privateAreaType, bool canExitArea, ushort music)
            : base(parent.ZoneId, parent.ZoneName, parent.RegionId, classPath, music, music, music, parent.isIsolated, parent.isInn, parent.canRideChocobo, parent.canStealth, true)
        {
            this.ParentZone = parent;
            this.PrivateAreaName = privateAreaName;
            this.PrivateAreaType = privateAreaType;
        }

        public override string GetPrivateAreaName()
        {
            return PrivateAreaName;
        }

        public override int GetPrivateAreaType()
        {
            return PrivateAreaType;
        }

        public bool CanExitPrivateArea()
        {
            return CanExitArea;
        }

        public override bool IsPublic()
        {
            return false;
        }

        public Zone GetParentZone()
        {
            return ParentZone;
        }

        public override SubPacket CreateScriptBindPacket()
        {
            List<LuaParam> lParams;

            string path = className;

            string realClassName = className.Substring(className.LastIndexOf("/") + 1);

            lParams = LuaUtils.CreateLuaParamList(classPath, false, true, ZoneName, PrivateAreaName, PrivateAreaType, canRideChocobo ? (byte)1 : (byte)0, canStealth, isInn, false, false, false, false, false, false);
            ActorInstantiatePacket.BuildPacket(Id, Name, realClassName, lParams).DebugPrintSubPacket();
            return ActorInstantiatePacket.BuildPacket(Id, Name, realClassName, lParams);
        }


        public void AddSpawnLocation(SpawnLocation spawn)
        {
            mSpawnLocations.Add(spawn);
        }

        public void SpawnAllActors()
        {
            foreach (SpawnLocation spawn in mSpawnLocations)
                SpawnActor(spawn);
        }
    }
}
