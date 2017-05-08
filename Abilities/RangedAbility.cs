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

        public virtual float BonusRange
        {
            get
            {
                var bonusRange = 0.0f;
                var unit = this.Ability.Owner as Unit;
                if (unit != null)
                {
                    // items
                    var aetherLense = unit.GetItemById(AbilityId.item_aether_lens);
                    if (aetherLense != null)
                    {
                        bonusRange += aetherLense.GetAbilitySpecialData("cast_range_bonus");
                    }

                    // talents
                    foreach (var talent in unit.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_cast_range_")))
                    {
                        bonusRange += talent.GetAbilitySpecialData("value");
                    }
                }

                return bonusRange;
            }
        }

        public override float Range
        {
            get
            {
                var castRange = (float)this.Ability.CastRange;

                castRange += this.BonusRange;

                return castRange;
            }
        }
    }
}