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

namespace Meteor.Navigation
{
    /// <summary>
    /// The client places actors it doesn't control on the ground with one ray straight down from above
    /// their drawn height; the nearest hit wins, either side up. That is all the collision they have.
    /// </summary>
    public static class ClientProbe
    {
        /// <summary>Retail uses 2. At 1, a cave ceiling 1-2 above the floor isn't taken for ground.</summary>
        public const float RayUp = 1.0f;
        public const float RayDown = 10.0f;

        /// <summary>The probe skips water, sea planes and some cave ceilings (material bit 2), and only
        /// sees collision kinds 1 and 16.</summary>
        public static bool IsVisible(int material, int kind) => (material & 2) == 0 && (kind == 1 || kind == 16);
    }
}
