// <copyright file="venomancer_poison_nova.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_venomancer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class venomancer_poison_nova : ActiveAbility, IHasDot, IAreaOfEffectAbility
    {
        public venomancer_poison_nova(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "duration");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "radius");
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
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}