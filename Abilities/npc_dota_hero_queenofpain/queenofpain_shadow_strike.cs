﻿// <copyright file="queenofpain_shadow_strike.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class queenofpain_shadow_strike : RangedAbility, IHasDot, IAreaOfEffectAbility
    {
        public queenofpain_shadow_strike(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration_tooltip");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float Radius
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_queen_of_pain);
                if (talent?.Level > 0)
                {
                    return talent.GetAbilitySpecialData("value");
                }

                return 0;
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration_damage");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_queenofpain_shadow_strike";

        public float TickRate { get; } = 3.0f;

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("strike_damage");
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
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}