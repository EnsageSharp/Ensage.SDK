// <copyright file="bane_fiends_grip.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bane
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class bane_fiends_grip : RangedAbility, IHasDot
    {
        public bane_fiends_grip(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                var duration = this.Ability.GetAbilitySpecialData("fiend_grip_duration");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_bane_3);
                if (talent?.Level > 0)
                {
                    duration += talent.GetAbilitySpecialData("value");
                }

                return duration;
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("fiend_grip_damage") * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_bane_fiends_grip";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("fiend_grip_tick_interval");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.RawTickDamage;
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