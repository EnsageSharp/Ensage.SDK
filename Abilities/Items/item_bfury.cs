// <copyright file="item_bfury.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_bfury : RangedAbility
    {
        public item_bfury(Item item)
            : base(item)
        {
        }

        public float CastRangeOnWard
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cast_range_ward");
            }
        }
    }
}