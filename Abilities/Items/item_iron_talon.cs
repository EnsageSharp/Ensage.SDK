// <copyright file="item_iron_talon.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_iron_talon : RangedAbility
    {
        public item_iron_talon(Item item)
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

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Pure;
            }
        }
    }
}