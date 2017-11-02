// <copyright file="venomancer_poison_nova.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_venomancer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class venomancer_poison_nova : AreaOfEffectAbility, IHasDot
    {
        public venomancer_poison_nova(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                var duration = this.Ability.GetAbilitySpecialData("duration");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_venomancer_4);
                if (talent?.Level > 0)
                {
                    duration += talent.GetAbilitySpecialData("value");
                }

                return duration;
            }
        }

        public bool HasInitialDamage { get; } = true;

        public override float Radius
        {
            get
            {
                var radius = base.Radius;

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_venomancer_6);
                if (talent?.Level > 0)
                {
                    radius += talent.GetAbilitySpecialData("value");
                }

                return radius;
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData(this.Owner.HasAghanimsScepter() ? "damage_scepter" : "damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_venomancer_poison_nova";

        public float TickRate { get; } = 1.0f;

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.Duration / this.TickRate));
        }
    }
}