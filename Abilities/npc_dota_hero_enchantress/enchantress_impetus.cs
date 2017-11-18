// <copyright file="enchantress_impetus.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enchantress
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class enchantress_impetus : OrbAbility
    {
        public enchantress_impetus(Ability ability)
            : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);
            var spellAmp = this.Owner.GetSpellAmplification();
            var distance = this.Ability.GetAbilitySpecialData("distance_damage_pct");

            var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_enchantress_4);
            if (talent != null && talent.Level > 0)
            {
                distance += talent.GetAbilitySpecialData("value");
            }

            if (targets.Any())
            {
                var target = targets.First();
                var targetDistance = Math.Min(this.Owner.Distance2D(target) + (this.Owner.HullRadius + target.HullRadius), this.Owner.AttackRange(target)) / 100;

                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                var distanceDamage = DamageHelpers.GetSpellDamage(distance, spellAmp, reduction);

                damage += targetDistance * distanceDamage;
            }

            return damage;
        }

        public override float GetDamage(Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            var damage = base.GetDamage(target);
            var spellAmp = this.Owner.GetSpellAmplification();
            var distance = this.Ability.GetAbilitySpecialData("distance_damage_pct");

            var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_enchantress_4);
            if (talent != null && talent.Level > 0)
            {
                distance += talent.GetAbilitySpecialData("value");
            }

            var targetDistance = Math.Min(this.Owner.Distance2D(target) + (this.Owner.HullRadius + target.HullRadius), this.Owner.AttackRange(target)) / 100;

            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            var distanceDamage = DamageHelpers.GetSpellDamage(distance, spellAmp, -reduction, damageModifier);

            return damage += targetDistance * distanceDamage;
        }
    }
}