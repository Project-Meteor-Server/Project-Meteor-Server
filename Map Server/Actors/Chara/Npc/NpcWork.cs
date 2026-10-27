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

namespace Meteor.Map.Actors.Chara
{
    class NpcWork
    {
        // npcWork.hateType, read by DepictionJudge:judgeNameplate.
        // 1 pale yellow, 2 orange; any other value means claimed: red if the mob's MonsterParty
        // occupancy group (0x0187) contains the viewer, purple otherwise. 0 is not "none" (purple).
        public const byte HATE_TYPE_UNCLAIMED = 1;
        public const byte HATE_TYPE_ENGAGED = 2;    // engaged without a claim (no claimer party)
        public const byte HATE_TYPE_CLAIMED = 3;
        public const byte HATE_TYPE_DEAD = 4;       // retail sends it with the death

        public ushort pushCommand;
        public int pushCommandSub;
        public byte pushCommandPriority;
        public byte hateType = HATE_TYPE_UNCLAIMED;                
    }
}
