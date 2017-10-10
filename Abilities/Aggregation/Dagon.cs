// <copyright file="Dagon.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Aggregation
{
    using Ensage.SDK.Extensions;

    public abstract class Dagon : RangedAbility
    {
        protected Dagon(Item item)
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