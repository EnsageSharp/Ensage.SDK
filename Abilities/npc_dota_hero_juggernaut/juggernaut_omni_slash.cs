// <copyright file="juggernaut_omni_slash.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.Common.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_juggernaut
{
    using System.Linq;
    using System;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class juggernaut_omni_slash : RangedAbility, IAreaOfEffectAbility, IHasDot, IHasModifier
    {
        public juggernaut_omni_slash(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                var duration = this.Ability.GetAbilitySpecialData(this.Owner.HasAghanimsScepter() ? "duration" : "duration_scepter");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_juggernaut_2);
                if (talent?.Level > 0)
                {
                    duration += talent.GetAbilitySpecialData("value");
                }

                return duration;
            }
        }

        public bool HasInitialDamage { get; } = true;

        public override bool IsReady
        {
            get
            {
                return this.Ability.IsActivated && base.IsReady;
            }
        }

        public string ModifierName { get; } = "modifier_juggernaut_omnislash";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("omni_slash_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bonus_damage") + (this.Owner.DamageAverage + this.Owner.BonusDamage);
            }
        }

        public string TargetModifierName { get; } = string.Empty;

        public float TickRate
        {
            get
            {
                return (float)this.Owner.AttackRate() * this.Ability.GetAbilitySpecialData("attack_rate_multiplier");
            }
        }

        public float JumpCount
        {
            get
            {
                return (float)Math.Ceiling(this.DamageDuration / this.TickRate);
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
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
            return this.GetTickDamage(targets) * JumpCount;
        }
    }
}