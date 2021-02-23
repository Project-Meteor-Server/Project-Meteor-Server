using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteor.Map.DataObjects
{
    class Recipe
    {
        public readonly uint resultItemID;
        public readonly uint resultQuantity;
        public readonly byte[] allowedCrafters;
        public readonly byte tier;

        public Recipe(uint resultItemID, uint resultQuantity, byte[] allowedCrafters, byte tier)
        {
            this.resultItemID = resultItemID;
            this.resultQuantity = resultQuantity;
            this.allowedCrafters = allowedCrafters;
            this.tier = tier;
        }
    }
}
