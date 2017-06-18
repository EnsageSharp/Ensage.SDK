// <copyright file="sniper_assassinate.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_sniper
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class sniper_assassinate : RangedAbility, IAreaOfEffectAbility
    {
        public sniper_assassinate(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Owner.HasAghanimsScepter() ? this.Ability.GetAbilitySpecialData("scepter_radius") : 0;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                if (!this.Owner.HasAghanimsScepter())
                {
                    return base.RawDamage;
                }

                var critBonus = this.Ability.GetAbilitySpecialData("scepter_crit_bonus"); // 280
                var damage = this.Owner.MinimumDamage + this.Owner.BonusDamage;
                return damage * (critBonus / 100.0f);
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!this.Owner.HasAghanimsScepter())
            {
                return base.GetDamage(targets);
            }

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = target.DamageResist;
                totalDamage = DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}