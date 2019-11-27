// <copyright file="huskar_life_break.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class huskar_life_break : RangedAbility, IHasModifier, IHasTargetModifier, IHasHealthCost
    {
        public huskar_life_break(Ability ability)
            : base(ability)
        {
        }

        public float HealthCost
        {
            get
            {
                var percent = this.Ability.GetAbilitySpecialData("health_cost_percent");
                return this.Owner.Health * percent;
            }
        }

        public string ModifierName { get; } = "modifier_huskar_life_break_charge";

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("charge_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_huskar_life_break_slow";
        public string ScepterModifierName { get; } = "modifier_huskar_life_break_taunt";

        protected override float RawDamage
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return this.Ability.GetAbilitySpecialData("health_damage_scepter");
                }

                return this.Ability.GetAbilitySpecialData("health_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                damage *= targets.First().Health;
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }
    }
}