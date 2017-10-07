// <copyright file="zuus_static_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class zuus_static_field : PassiveAbility, IAreaOfEffectAbility
    {
        public zuus_static_field(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                var damagePercent = this.Ability.GetAbilitySpecialData("damage_health_pct");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_zeus);
                if (talent != null && talent.Level > 0)
                {
                    damagePercent += talent.GetAbilitySpecialData("value");
                }

                return damagePercent / 100;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damagePercent = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets.Where(x => x.Distance2D(this.Owner) <= this.Radius))
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                var damage = damagePercent * target.Health;
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}