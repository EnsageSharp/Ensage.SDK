// <copyright file="huskar_burning_spear.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class huskar_burning_spear : OrbAbility, IHasDot, IHasHealthCost
    {
        public huskar_burning_spear(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Duration;
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float HealthCost
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("health_cost");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "burn_damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_huskar_burning_spear_counter";

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}