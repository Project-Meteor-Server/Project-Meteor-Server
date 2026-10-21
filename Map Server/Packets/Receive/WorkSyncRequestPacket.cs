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
using System.IO;
using System.Text;

namespace Meteor.Map.packets.receive
{
    class WorkSyncRequestPacket
    {
        public const ushort OPCODE = 0x012F;
        public const uint PACKET_SIZE = 0x48;

        public bool invalidPacket = false;

        public uint actorID;
        public string propertyName;
        public ushort from, to;
        public bool requestingBitfield = false;
       
        public WorkSyncRequestPacket(byte[] data)
        {
            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryReader binReader = new BinaryReader(mem))
                {
                    try{
                        actorID = binReader.ReadUInt32();
                        if (binReader.PeekChar() == 9)
                        {
                            binReader.ReadByte();
                            from = binReader.ReadUInt16();
                            to = binReader.ReadUInt16();
                        }

                        byte currentByte;
                        int size = 0;
                        long strPos = binReader.BaseStream.Position;
                        while ((currentByte = binReader.ReadByte()) != 0 && size <= 0x20)
                            size++;

                        binReader.BaseStream.Seek(strPos, SeekOrigin.Begin);
                        byte[] str = binReader.ReadBytes(size);
                        propertyName = Encoding.ASCII.GetString(str);
                    }
                    catch (Exception){
                        invalidPacket = true;
                    }
                }
            }
        }
    }
}
