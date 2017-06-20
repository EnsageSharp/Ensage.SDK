// <copyright file="item_dagon_2.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_dagon_2 : RangedAbility
    {
        public item_dagon_2(Item item)
            : base(item)
        {
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}