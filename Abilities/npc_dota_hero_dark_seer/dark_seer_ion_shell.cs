// <copyright file="dark_seer_ion_shell.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_seer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class dark_seer_ion_shell : ActiveAbility, IHasModifier, IHasDot, IAreaOfEffectAbility
    {
        public dark_seer_ion_shell(Ability ability)
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

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public string ModifierName { get; } = "modifier_dark_seer_ion_shell";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage_per_second") * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = string.Empty;

        public float TickRate { get; } = 0.1f;

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