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
using System.Collections.Generic;
using System.IO;

namespace Meteor.Map.packets.send.group
{
    class ContentMembersX08Packet
    {
        public const ushort OPCODE = 0x0183;
        public const uint PACKET_SIZE = 0x1B8;

        public static SubPacket buildPacket(uint playerActorID, uint locationCode, ulong sequenceId, List<GroupMember> entries, ref int offset)
        {
            byte[] data = new byte[PACKET_SIZE - 0x20];

            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryWriter binWriter = new BinaryWriter(mem))
                {
                    //Write List Header
                    binWriter.Write((UInt64)locationCode);
                    binWriter.Write((UInt64)sequenceId);
                    //Write Entries
                    int max = 8;
                    if (entries.Count-offset < 8)
                        max = entries.Count - offset;
                    for (int i = 0; i < max; i++)
                    {
                        binWriter.Seek(0x10 + (0xC * i), SeekOrigin.Begin);

                        // entries[offset], not [i]: a roster past one packet must not repeat the first members.
                        GroupMember entry = entries[offset];
                        binWriter.Write((UInt32)entry.actorId);
                        binWriter.Write((UInt32)1001); //Layout ID
                        binWriter.Write((UInt32)1); //?

                        offset++;
                    }
                    //Write Count
                    binWriter.Seek(0x10 + (0xC * 8), SeekOrigin.Begin);
                    binWriter.Write(max);
                }
            }

            return new SubPacket(OPCODE, playerActorID, data);
        }
            // The member list retail sends for a MonsterParty (0x98 bytes): 0x0C-byte entries {actorId,
        // unknown, 1 = online} at +0x10, count at +0x70. Retail's middle value varies by zone.
        public static SubPacket buildCompactPacket(uint playerActorID, uint locationCode, ulong sequenceId, List<uint> actorIds, uint unknown)
        {
            byte[] data = new byte[0x98 - 0x20];

            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryWriter binWriter = new BinaryWriter(mem))
                {
                    binWriter.Write((UInt64)locationCode);
                    binWriter.Write((UInt64)sequenceId);
                    int max = Math.Min(8, actorIds.Count);
                    for (int i = 0; i < max; i++)
                    {
                        binWriter.Seek(0x10 + (0xC * i), SeekOrigin.Begin);
                        binWriter.Write((UInt32)actorIds[i]);
                        binWriter.Write((UInt32)unknown);
                        binWriter.Write((UInt32)1);
                    }
                    binWriter.Seek(0x10 + (0xC * 8), SeekOrigin.Begin);
                    binWriter.Write((UInt32)max);
                }
            }

            return new SubPacket(OPCODE, playerActorID, data);
        }
    }
}
