// <copyright file="TargetAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public abstract class TargetAbility : BaseAbility
    {
        protected TargetAbility(Ability ability)
            : base(ability)
        {
        }

        public virtual float Range
        {
            get
            {
                var castRange = (float)this.Ability.CastRange;

                var unit = this.Ability.Owner as Unit;
                if (unit != null)
                {
                    // items
                    var aetherLense = unit.GetItemById(AbilityId.item_aether_lens);
                    if (aetherLense != null)
                    {
                        castRange += aetherLense.GetAbilitySpecialData("cast_range_bonus");
                    }

                    // talents
                    foreach (var talent in
                        unit.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_cast_range_")))
                    {
                        castRange += talent.GetAbilitySpecialData("value");
                    }
                }

                return castRange;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var level = this.Ability.Level;
            if (level == 0)
            {
                return 0;
            }

            var damage = this.Ability.GetDamage(level - 1);

            var target = targets.First();
            if (!this.Ability.CanAffectTarget(target))
            {
                return 0;
            }

            var amplify = this.Ability.GetSpellAmp();
            var reduction = this.Ability.GetDamageReduction(target);

            return damage * (1.0f + amplify) * (1.0f - reduction);
        }
    }
}