// <copyright file="disruptor_static_storm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_disruptor
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class disruptor_static_storm : CircleAbility, IHasDot
    {
        public disruptor_static_storm(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return UnitState.Silenced | UnitState.Muted;
                }

                return UnitState.Silenced;
            }
        }

        public float DamageDuration
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return this.Ability.GetAbilitySpecialData("duration_scepter");
                }

                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                // average damage
                return 25f;
            }
        }

        public string TargetModifierName { get; } = "modifier_disruptor_static_storm";

        public float TickRate { get; } = 0.25f;

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