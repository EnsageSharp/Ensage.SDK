// <copyright file="RangedAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;

    public abstract class RangedAbility : ActiveAbility
    {
        protected RangedAbility(Ability ability)
            : base(ability)
        {
        }

        public override float CastRange
        {
            get
            {
                var bonusRange = 0.0f;

                var aetherLense = this.Owner.GetItemById(AbilityId.item_aether_lens);
                if (aetherLense != null)
                {
                    bonusRange += aetherLense.GetAbilitySpecialData("cast_range_bonus");
                }

                return this.BaseCastRange + bonusRange;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.GetCastRange();
            }
        }
    }
}