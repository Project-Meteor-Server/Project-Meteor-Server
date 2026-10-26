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
using System;
using System.IO;

namespace Meteor.Map.packets.send.actor.battle
{
    // 0x0195: who a monster's enmity points at, and the recipient's share of it (0xFFFF = none).
    // The source must be the monster: a player source crashes the client. Send only after the mob's
    // 0x00CC. While any mob is flagged the client can't target unclaimed mobs with the All circle,
    // so every flagged mob must be cleared on disengage, leash and death.
    class SetEnmityIndicatorPacket
    {
        public const ushort OPCODE = 0x0195;
        public const uint PACKET_SIZE = 0x28;

        public const uint NO_TARGET = 0xE0000000;
        public const ushort FLAG_NONE = 0xFFFF;
        public const ushort FLAG_ENGAGED = 100;   // recipient is the target / top of the list

        public static SubPacket BuildPacket(uint monsterActorId, uint playerActorId, uint targetActorId, ushort flag)
        {
            byte[] data = new byte[PACKET_SIZE - 0x20];

            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryWriter binWriter = new BinaryWriter(mem))
                {
                    binWriter.Write((uint)targetActorId);
                    binWriter.Write((ushort)flag);
                    binWriter.Write((ushort)0);
                }
            }

            return new SubPacket(new SubPacket(OPCODE, monsterActorId, data), playerActorId);
        }
    }
}
