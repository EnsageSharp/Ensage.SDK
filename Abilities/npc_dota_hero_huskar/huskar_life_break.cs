// <copyright file="huskar_life_break.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{

    using System.Linq;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;


    public class huskar_life_break : RangedAbility, IHasTargetModifier, IHasModifier, IHasLifeCost
    {
        public huskar_life_break(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName => "modifier_huskar_life_break_charge";
        public string TargetModifierName => "modifier_huskar_life_break_slow";
        public float HealthCostPercent => Ability.GetAbilitySpecialData("tooltip_health_damage");
        public float HealthDamagePercent => Ability.GetAbilitySpecialData("tooltip_health_damage");
        public float HealthCost => GetSelfDamage();

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.GetRawDamage();
            var amplify = this.Owner.GetSpellAmplification();
            var target = targets.First();
            if (targets.Any())
            {
                return damage;
            }
            var reduction = this.Ability.GetDamageReduction(target);
            totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);

            return totalDamage;
        }

        private float GetSelfDamage()
        {
            var selfDamage = 0.0f;

            var damage = this.GetRawDamage();
            var amplify = 0.0f;
            var reduction = this.Ability.GetDamageReduction(this.Owner);

            selfDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);

            return selfDamage >= this.Owner.Health ? this.Owner.Health : selfDamage;
            
        }
    }
}