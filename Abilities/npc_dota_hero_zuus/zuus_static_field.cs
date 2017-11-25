﻿// <copyright file="zuus_static_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

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
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage_health_pct") / 100f;
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

        public override float GetDamage([NotNull] Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            if (target.Distance2D(this.Owner) > this.Radius)
            {
                return 0;
            }

            if (targetHealth == float.MinValue)
            {
                targetHealth = target.Health;
            }

            var damagePercent = this.RawDamage;
            var damage = damagePercent * targetHealth;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, -reduction, damageModifier);
        }
    }
}