// <copyright file="dazzle_poison_touch.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dazzle
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class dazzle_poison_touch : RangedAbility, IHasDot, IAreaOfEffectAbility
    {
        public dazzle_poison_touch(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("end_radius");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("start_radius");
            }
        }

        public float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("end_distance");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_dazzle_poison_touch";

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawTickDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}