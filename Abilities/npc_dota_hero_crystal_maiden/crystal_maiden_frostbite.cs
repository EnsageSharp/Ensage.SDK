// <copyright file="crystal_maiden_frostbite.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_crystal_maiden
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class crystal_maiden_frostbite : RangedAbility, IHasDot
    {
        public crystal_maiden_frostbite(Ability ability)
            : base(ability)
        {
        }

        public float CreepDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("creep_duration") + this.BonusDuration;
            }
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration") + this.BonusDuration;
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_second_tooltip");
            }
        }

        public string TargetModifierName { get; } = "modifier_crystal_maiden_frostbite";

        public float TickRate { get; } = 0.5f;

        private float BonusDuration
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_crystal_maiden_1);
                if (talent != null && talent.Level > 0)
                {
                    return talent.GetAbilitySpecialData("value");
                }

                return 0;
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
                reduction = this.Ability.GetDamageReduction(targets.First());
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            var target = targets.FirstOrDefault();
            var duration = target != null && target.IsNeutral ? this.CreepDuration : this.Duration;

            return this.GetTickDamage(targets) * (duration / this.TickRate);
        }
    }
}