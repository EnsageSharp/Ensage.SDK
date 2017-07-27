// <copyright file="venomancer_venomous_gale.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_venomancer
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class venomancer_venomous_gale : LineAbility, IHasDot
    {
        public venomancer_venomous_gale(Ability ability)
            : base(ability)
        {
        }

        public float Duration
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
                return this.Ability.GetAbilitySpecialData("tick_damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_venomancer_venomous_gale";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("tick_interval");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("strike_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First());
            }

            return DamageHelpers.GetSpellDamage(this.RawDamage, amplify, reduction);
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
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.Duration / this.TickRate));
        }
    }
}