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

namespace Meteor.Map.actors.area
{
    class SpawnLocation
    {
        public uint classId;
        public string uniqueId;
        public uint zoneId;
        public string privAreaName;
        public int privAreaLevel;
        public float x;
        public float y;
        public float z;
        public float rot;
        public uint motionPack;

        public uint mapObjLayoutId;
        public uint mapObjInstanceId;

        public SpawnLocation(uint classId, string uniqueId, uint zoneId, string privAreaName, int privAreaLevel, float x, float y, float z, float rot, uint animId, uint mapObjLayoutId, uint mapObjInstanceId)
        {
            this.classId = classId;
            this.uniqueId = uniqueId;
            this.zoneId = zoneId;
            this.privAreaName = privAreaName;
            this.privAreaLevel = privAreaLevel;
            this.x = x;
            this.y = y;
            this.z = z;
            this.rot = rot;
            this.motionPack = animId;
            this.mapObjLayoutId = mapObjLayoutId;
            this.mapObjInstanceId = mapObjInstanceId;
    }
    }
}
