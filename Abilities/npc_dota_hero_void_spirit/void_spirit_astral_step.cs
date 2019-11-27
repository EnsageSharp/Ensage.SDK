// <copyright file="void_spirit_astral_step.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Helpers;

namespace Ensage.SDK.Abilities.npc_dota_hero_void_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Prediction.Collision;

    public class void_spirit_astral_step : LineAbility, IHasTargetModifier
    {
        public void_spirit_astral_step(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override float Speed
        {
            get
            {
                return float.MaxValue;

            }
        }

        protected override float RawDamage
        {
            get
            {
                return Ability.GetAbilitySpecialData("pop_damage");

            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var talent = Owner.GetAbilityById(AbilityId.special_bonus_unique_void_spirit_8);
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var critDamage = talent?.Level > 0 ? Owner.MinimumDamage : 0;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                var critReduction = Ability.GetDamageReduction(target, DamageType.Physical);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction) + DamageHelpers.GetSpellDamage(critDamage, 0, critReduction);
            }

            return totalDamage;
        }

        public string TargetModifierName { get; } = "modifier_void_spirit_astral_step_debuff";
    }
}