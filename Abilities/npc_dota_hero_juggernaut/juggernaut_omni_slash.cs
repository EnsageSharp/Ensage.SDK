// <copyright file="juggernaut_omni_slash.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_juggernaut
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class juggernaut_omni_slash : RangedAbility, IAreaOfEffectAbility, IHasDot, IHasModifier
    {
        public juggernaut_omni_slash(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.IsActivated && base.CanBeCasted;
            }
        }

        public float Duration
        {
            get
            {
                var jumps = this.Ability.GetAbilitySpecialData(this.Owner.HasAghanimsScepter() ? "omni_slash_jumps_scepter" : "omni_slash_jumps");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_juggernaut_2);
                if (talent?.Level > 0)
                {
                    jumps += talent.GetAbilitySpecialData("value");
                }

                return (jumps - 1) * this.TickRate;
            }
        }

        public bool HasInitialDamage { get; } = true;

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
                return this.Ability.GetAbilitySpecialData("omni_slash_damage", 1);
            }
        }

        public string TargetModifierName { get; } = string.Empty;

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("omni_slash_bounce_tick");
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
            return this.GetTickDamage(targets) * (this.Duration / this.TickRate);
        }
    }
}