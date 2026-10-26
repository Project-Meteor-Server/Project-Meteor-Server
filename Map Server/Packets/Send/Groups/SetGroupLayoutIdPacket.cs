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
using System.Text;

namespace Meteor.Map.packets.send.groups
{
    // 0x018B: one member entry of a group the client already has, laid out as in the member lists.
    // An NPC member is named by its display name id; a member with a name string sends 0xFFFFFFFF.
    class SetGroupLayoutIdPacket
    {
        public const ushort OPCODE = 0x018B;
        public const uint PACKET_SIZE = 0x58;

        public static SubPacket BuildPacket(uint playerActorId, ulong groupId, uint memberActorId, uint displayNameId, uint layoutId, bool flag1, string name)
        {
            byte[] data = new byte[PACKET_SIZE - 0x20];

            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryWriter binWriter = new BinaryWriter(mem))
                {
                    binWriter.Write((UInt64)groupId);
                    binWriter.Write((UInt32)memberActorId);
                    binWriter.Write((UInt32)displayNameId);
                    binWriter.Write((UInt32)layoutId);
                    binWriter.Write((Byte)(flag1 ? 1 : 0));
                    binWriter.Write((Byte)1);
                    byte[] nameBytes = Encoding.ASCII.GetBytes(name ?? "");
                    binWriter.Write(nameBytes, 0, Math.Min(nameBytes.Length, 0x21));
                }
            }

            return new SubPacket(OPCODE, playerActorId, data);
        }
    }
}
