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
using Meteor.Map.lua;
using Meteor.Map.packets.send.actor;
using System.Collections.Generic;

namespace Meteor.Map.Actors
{
    class DebugProg2 : Actor
    {

        public DebugProg2()
            : base(0x5FF80011)
        {
            this.LocalizedDisplayName = 0;
            this.DisplayName = "debug2";

            this.Name = "debug2";
            this.className = "Debug2";
        }

        public override SubPacket CreateScriptBindPacket()
        {
            List<LuaParam> lParams;
            lParams = LuaUtils.CreateLuaParamList("/System/Debug.prog", false, false, false, false, true, 0xC51F, true, true);
            return ActorInstantiatePacket.BuildPacket(Id, Name, className, lParams);
        }

        public override List<SubPacket> GetSpawnPackets()
        {
            List<SubPacket> subpackets = new List<SubPacket>();
            subpackets.Add(CreateAddActorPacket(0x11));            
            subpackets.Add(CreateSpeedPacket());
            subpackets.Add(CreateSpawnPositonPacket(0x1));
            subpackets.Add(CreateNamePacket());
            subpackets.Add(CreateStatePacket());
            subpackets.Add(CreateIsZoneingPacket());
            subpackets.Add(CreateScriptBindPacket());
            return subpackets;
        }

    }
}
