// <copyright file="warlock_shadow_word.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_warlock
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class warlock_shadow_word : RangedAbility, IHasDot, IHasHealthRestore
    {
        public warlock_shadow_word(Ability ability)
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

        public float RawTickDamage
        {
            get
            {
                return this.RawDamage;
            }
        }

        public string TargetModifierName { get; } = "modifier_warlock_shadow_word";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("tick_interval");
            }
        }

        public float TotalHealthRestore
        {
            get
            {
                return this.RawTickDamage * this.Duration;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            return 0;
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();

            var totalDamage = 0.0f;
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