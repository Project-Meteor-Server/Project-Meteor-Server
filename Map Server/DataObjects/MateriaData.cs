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

using MySql.Data.MySqlClient;

using MySql.Data.MySqlClient;

namespace Meteor.Map.DataObjects
{
    // Grades 0-3 are the tier I materia, 4-7 tier II, 8-11 tier III, 12-15 tier IV.
    class MateriaData
    {
        public const int GRADE_COUNT = 16;

        public readonly int id;
        public readonly int bonus1Type;
        public readonly int bonus2Type;
        private readonly short[] bonus1Values = new short[GRADE_COUNT];
        private readonly short[] bonus2Values = new short[GRADE_COUNT];

        public MateriaData(MySqlDataReader reader)
        {
            id = reader.GetInt32("id");
            bonus1Type = reader.GetInt32("bonus1Type");
            bonus2Type = reader.GetInt32("bonus2Type");
            for (int i = 0; i < GRADE_COUNT; i++)
            {
                bonus1Values[i] = reader.GetInt16("bonus1Grade" + i + "Value");
                bonus2Values[i] = reader.GetInt16("bonus2Grade" + i + "Value");
            }
        }

        public int GetBonusValue(int bonus, int grade)
        {
            if (grade < 0 || grade >= GRADE_COUNT)
                return 0;
            return bonus == 1 ? bonus1Values[grade] : bonus2Values[grade];
        }
    }
}
