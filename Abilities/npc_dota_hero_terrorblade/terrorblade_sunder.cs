// <copyright file="terrorblade_sunder.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_terrorblade
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;

    public class terrorblade_sunder : RangedAbility
    {
        public terrorblade_sunder(Ability ability)
            : base(ability)
        {
        }

        public float OwnerHealth(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var target = targets.First();
            var targetHealthPercent = (float)target.Health / target.MaximumHealth;
            var ownerHealthPercent = (float)this.Owner.Health / this.Owner.MaximumHealth;

            var percent = targetHealthPercent - ownerHealthPercent;

            var health = 0.0f;
            if (percent >= 0)
            {
                health = this.Owner.MaximumHealth * percent;
            }

            return health;
        }

        public float TargetHealth(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var target = targets.First();
            var ownerHealthPercent = (float)this.Owner.Health / this.Owner.MaximumHealth;
            var targetHealthPercent = (float)target.Health / target.MaximumHealth;

            var percent = ownerHealthPercent - targetHealthPercent;

            var health = 0.0f;
            if (percent >= 0)
            {
                health = target.MaximumHealth * percent;
            }

            return health;
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var minDamagePercent = this.Ability.GetAbilitySpecialData("hit_point_minimum_pct");

            var target = targets.First();
            var targetDamagePercent = Math.Max((float)target.Health / target.MaximumHealth, minDamagePercent / 100);
            var ownerDamagePercent = Math.Max((float)this.Owner.Health / this.Owner.MaximumHealth, minDamagePercent / 100);

            var percent = targetDamagePercent - ownerDamagePercent;

            var damage = 0.0f;
            if (percent >= 0)
            {
                damage = (float)Math.Ceiling(target.MaximumHealth * percent);
            }

            return damage;
        }
    }
}