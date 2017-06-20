// <copyright file="item_ward_dispenser.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_ward_dispenser : RangedAbility
    {
        public item_ward_dispenser(Item item)
            : base(item)
        {
        }

        public uint ObserversCount
        {
            get
            {
                return this.Item.CurrentCharges;
            }
        }

        public uint SentriesCount
        {
            get
            {
                return this.Item.SecondaryCharges;
            }
        }
    }
}