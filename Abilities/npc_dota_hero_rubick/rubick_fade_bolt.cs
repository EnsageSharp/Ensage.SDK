// <copyright file="rubick_fade_bolt.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rubick
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class rubick_fade_bolt : RangedAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public rubick_fade_bolt(Ability ability)
            : base(ability)
        {
        }

        public virtual float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_rubick_fade_bolt_debuff";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amp = this.Owner.GetSpellAmplification();
            var jumpReduction = this.Ability.GetAbilitySpecialData("jump_damage_reduction_pct") / 100.0f;

            var currentReduction = 0.0f;
            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amp, -reduction, -currentReduction);
                currentReduction += jumpReduction;
            }

            return totalDamage;
        }
    }
}