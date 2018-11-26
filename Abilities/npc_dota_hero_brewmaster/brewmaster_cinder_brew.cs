// <copyright file="brewmaster_cinder_brew.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_brewmaster
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Abilities.Components;

    public class brewmaster_cinder_brew : CircleAbility, IHasTargetModifier, IHasDot, IHasProcChance
    {
        public brewmaster_cinder_brew(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_brewmaster_cinder_brew";

        public float DamageDuration { get; } = 3.0f;
        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("ignite_dps");
            }
        }

        public float TickRate { get; } = 0.5f;

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage / 2.0f; // Internal data gives damage per second meanwhile ability ticks every half second.
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
            return this.GetTickDamage(targets) * (DamageDuration / TickRate);
        }

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                return Ability.GetAbilitySpecialData("self_attack_chance");
            }
        }
    }
}