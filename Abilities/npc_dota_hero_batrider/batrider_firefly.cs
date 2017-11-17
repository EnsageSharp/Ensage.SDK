// <copyright file="batrider_firefly.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_batrider
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class batrider_firefly : ActiveAbility, IHasDot, IAreaOfEffectAbility
    {
        public batrider_firefly(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                var duration = this.Ability.GetAbilitySpecialData("duration");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_batrider_1);
                if (talent?.Level > 0)
                {
                    duration += talent.GetAbilitySpecialData("value");
                }

                return duration;
            }
        }

        public bool HasInitialDamage { get; } = false;

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
                return this.Ability.GetAbilitySpecialData("damage_per_second") * this.TickRate;
            }
        }

        public string TargetModifierName { get; set; }

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("tick_interval");
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