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

namespace Meteor.Map.packets.send.groups
{
    // 0x0187: which group occupies (claims) another. For a mob claim the occupied group is its
    // MonsterParty and the occupying group the claimer's party (0 = unclaimed); the client shows a
    // claimed mob red when that party holds the viewer, purple otherwise. Retail sends a clear and
    // then a set on a claim, and a clear only on a reset (wipe or leash), never at a kill.
    class SetOccupancyGroupPacket
    {
        public const ushort OPCODE = 0x0187;
        public const uint PACKET_SIZE = 0x60;

        public static SubPacket BuildPacket(uint playerActorId, ulong occupiedGroupId, uint occupiedGroupType, ulong occupyingGroupId)
        {
            byte[] data = new byte[PACKET_SIZE - 0x20];

            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryWriter binWriter = new BinaryWriter(mem))
                {
                    binWriter.Write((UInt64)occupiedGroupId);
                    binWriter.Write((UInt32)occupiedGroupType);
                    binWriter.Write((UInt32)0);
                    binWriter.Write((UInt64)occupyingGroupId);
                    binWriter.Write((UInt32)0xFFFFFFFF);
                }
            }

            return new SubPacket(OPCODE, playerActorId, data);
        }
    }
}
