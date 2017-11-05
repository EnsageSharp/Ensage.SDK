// <copyright file="nyx_assassin_mana_burn.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using System;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class nyx_assassin_mana_burn : RangedAbility
    {
        public nyx_assassin_mana_burn(Ability ability)
            : base(ability)
        {
        }

        protected override float BaseCastRange
        {
            get
            {
                var modifier = this.Owner.GetModifierByName("modifier_nyx_assassin_burrow");
                if (modifier != null)
                {
                    var ability = this.Owner.GetAbilityById(AbilityId.nyx_assassin_burrow);
                    return ability.GetAbilitySpecialData("mana_burn_burrow_range_tooltip");
                }

                return this.Ability.CastRange;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var multiplier = this.Ability.GetAbilitySpecialData("float_multiplier");
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var hero = target as Hero;
                var manaBurnDamage = multiplier * hero.TotalIntelligence;

                var damage = Math.Min(manaBurnDamage, target.Mana);
                var reduction = target.MagicDamageResist;
                totalDamage = DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public override float GetDamage([NotNull] Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            var multiplier = this.Ability.GetAbilitySpecialData("float_multiplier");
            var amplify = this.Owner.GetSpellAmplification();

            var hero = target as Hero;
            var manaBurnDamage = multiplier * hero.TotalIntelligence;

            var damage = Math.Min(manaBurnDamage, target.Mana);
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, -reduction, damageModifier);
        }

        public float ManaBurn(params Unit[] targets)
        {
            var multiplier = this.Ability.GetAbilitySpecialData("float_multiplier");

            var totalMana = 0.0f;
            foreach (var target in targets)
            {
                var hero = target as Hero;
                totalMana = multiplier * hero.TotalIntelligence;
            }

            return totalMana;
        }
    }
}