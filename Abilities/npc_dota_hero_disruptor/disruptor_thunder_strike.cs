// <copyright file="disruptor_thunder_strike.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_disruptor
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class disruptor_thunder_strike : AreaOfEffectAbility, IHasDot
    {
        public disruptor_thunder_strike(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                var strikes = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "strikes");
                return strikes * this.TickRate;
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "strike_damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_disruptor_thunder_strike";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("strike_interval");
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