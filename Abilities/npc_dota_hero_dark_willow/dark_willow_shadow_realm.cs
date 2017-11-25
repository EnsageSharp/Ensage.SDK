// <copyright file="dark_willow_shadow_realm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_willow
{
    using System;
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dark_willow_shadow_realm : ActiveAbility, IHasModifier
    {
        public dark_willow_shadow_realm(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Invisible;

        public string ModifierName { get; } = "modifier_dark_willow_shadow_realm_buff";

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("attack_range_bonus") + this.Owner.AttackRange();
            }
        }

        protected override float RawDamage
        {
            get
            {
                var modifier = this.Owner.GetModifierByName(this.ModifierName);
                if (modifier == null)
                {
                    return 0;
                }

                var damage = this.Ability.GetAbilitySpecialData("damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_dark_willow_1);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                var timeMultiplier = Math.Min(modifier.ElapsedTime / this.Ability.GetAbilitySpecialData("max_damage_duration"), 1);

                return damage * timeMultiplier;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);

            if (targets.Any())
            {
                damage += this.Owner.GetAttackDamage(targets.First());
            }

            return damage;
        }
    }
}