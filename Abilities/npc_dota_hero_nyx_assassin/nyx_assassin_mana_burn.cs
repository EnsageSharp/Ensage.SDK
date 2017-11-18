// <copyright file="nyx_assassin_mana_burn.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

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

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("float_multiplier");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var target = targets.FirstOrDefault() as Hero;
            if (target == null)
            {
                return 0;
            }

            var multiplier = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            var manaBurn = multiplier * target.TotalIntelligence;
            var damage = Math.Min(manaBurn, target.Mana);

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public override float GetDamage(Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            var hero = target as Hero;
            if (hero == null)
            {
                return 0;
            }

            var multiplier = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(hero, this.DamageType);
            var manaBurn = multiplier * hero.TotalIntelligence;
            var damage = Math.Min(manaBurn, hero.Mana);

            return DamageHelpers.GetSpellDamage(damage, amplify, -reduction, damageModifier);
        }

        public float ManaBurn(params Unit[] targets)
        {
            var target = targets.FirstOrDefault() as Hero;
            if (target == null)
            {
                return 0;
            }

            var multiplier = this.RawDamage;
            var manaBurn = multiplier * target.TotalIntelligence;

            return Math.Min(manaBurn, target.Mana);
        }
    }
}