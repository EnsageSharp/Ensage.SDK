// <copyright file="item_quelling_blade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_quelling_blade : RangedAbility
    {
        public item_quelling_blade(Item item)
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