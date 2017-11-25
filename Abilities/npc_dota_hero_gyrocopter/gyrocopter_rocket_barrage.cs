// <copyright file="gyrocopter_rocket_barrage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_gyrocopter
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class gyrocopter_rocket_barrage : ActiveAbility, IAreaOfEffectAbility, IHasDot, IHasModifier
    {
        public gyrocopter_rocket_barrage(Ability ability)
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

        public string ModifierName { get; } = "modifier_gyrocopter_rocket_barrage";

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
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "rocket_damage");
            }
        }

        public string TargetModifierName { get; } = string.Empty;

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rockets_per_second") / 100f;
            }
        }

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
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}