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

using System.IO;
using DotRecast.Core;
using DotRecast.Core.Numerics;
using DotRecast.Detour;
using DotRecast.Detour.Io;

namespace Meteor.Map.navigation
{
    /// <summary>
    /// Reads RecastDemo "MSET" navmesh files. DotRecast's DtMeshSetReader stops at the first tile
    /// saved with ref 0, and these files save 0 for every tile, so each tile is added here with a new ref.
    /// </summary>
    static class MsetNavMeshLoader
    {
        private const int MSET_MAGIC = 'M' << 24 | 'S' << 16 | 'E' << 8 | 'T';
        private const int MSET_VERSION = 1;
        private const int MAX_VERTS_PER_POLY = 6;

        public static DtNavMesh Load(string path, out int tileCount, out int polyCount)
        {
            using (var reader = new BinaryReader(File.OpenRead(path)))
            {
                if (reader.ReadInt32() != MSET_MAGIC)
                    throw new InvalidDataException($"{path} is not a RecastDemo MSET navmesh");
                int version = reader.ReadInt32();
                if (version != MSET_VERSION)
                    throw new InvalidDataException($"{path}: unsupported MSET version {version}");

                int numTiles = reader.ReadInt32();
                var meshParams = new DtNavMeshParams
                {
                    orig = new RcVec3f(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                    tileWidth = reader.ReadSingle(),
                    tileHeight = reader.ReadSingle(),
                    maxTiles = reader.ReadInt32(),
                    maxPolys = reader.ReadInt32(),
                };

                var mesh = new DtNavMesh();
                var status = mesh.Init(in meshParams, MAX_VERTS_PER_POLY);
                if (status.Failed())
                    throw new InvalidDataException($"{path}: navmesh init failed ({status})");

                var dataReader = new DtMeshDataReader();
                tileCount = 0;
                polyCount = 0;

                for (int i = 0; i < numTiles; i++)
                {
                    reader.ReadUInt32(); // tile ref, always 0
                    int dataSize = reader.ReadInt32();
                    if (dataSize <= 0)
                        continue;

                    var buffer = new RcByteBuffer(reader.ReadBytes(dataSize));
                    buffer.Order(RcByteOrder.LITTLE_ENDIAN);
                    DtMeshData data = dataReader.Read(buffer, MAX_VERTS_PER_POLY, true);

                    status = mesh.AddTile(data, 0, 0, out _);
                    if (status.Failed())
                        throw new InvalidDataException($"{path}: tile {i} could not be added ({status})");

                    tileCount++;
                    polyCount += data.header.polyCount;
                }

                return mesh;
            }
        }
    }
}
