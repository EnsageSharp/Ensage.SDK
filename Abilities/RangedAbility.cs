// <copyright file="RangedAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;

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

                foreach (var talent in this.Owner.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_cast_range_")))
                {
                    bonusRange += talent.GetAbilitySpecialData("value");
                }

                var aetherLens = this.Owner.GetItemById(AbilityId.item_aether_lens);
                if (aetherLens != null)
                {
                    bonusRange += aetherLens.GetAbilitySpecialData("cast_range_bonus");
                }

                return this.BaseCastRange + bonusRange;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.CastRange;
            }
        }
    }
}