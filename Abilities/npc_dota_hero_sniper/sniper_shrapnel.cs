// <copyright file="sniper_shrapnel.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_sniper
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Prediction;

    public class sniper_shrapnel : CircleAbility, IHasDot
    {
        public sniper_shrapnel(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_delay");
            }
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float RawTickDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("shrapnel_damage");
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_sniper_1);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public string TargetModifierName { get; } = "modifier_sniper_shrapnel_slow";

        public float TickRate { get; } = 1.0f;

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
        }

        public override PredictionInput GetPredictionInput(params Unit[] targets)
        {
            var input = base.GetPredictionInput(targets);
            input.Delay = this.CastPoint + this.Ability.GetAbilitySpecialData("damage_delay");
            return input;
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
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}