// <copyright file="bristleback_quill_spray.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bristleback
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class bristleback_quill_spray : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public bristleback_quill_spray(Ability ability)
            : base(ability)
        {
        }

        public float Radius
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
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_bristleback_quill_spray";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("quill_base_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return this.RawDamage;
            }

            var amplify = this.Ability.SpellAmplification();
            var stackDamage = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "quill_stack_damage");
            var totalDamage = 0f;

            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                var stacks = target.GetModifierByName(this.TargetModifierName)?.StackCount ?? 0;
                totalDamage += DamageHelpers.GetSpellDamage(this.RawDamage + (stacks * stackDamage), amplify, reduction);
            }

            return totalDamage;
        }
    }
}