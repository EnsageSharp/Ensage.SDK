// <copyright file="item_ethereal_blade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class item_ethereal_blade : RangedAbility, IHasTargetModifier
    {
        public item_ethereal_blade(Item item)
            : base(item)
        {
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_item_ethereal_blade_ethereal";

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.GetRawDamage();

            var amplify = this.Owner.GetSpellAmplification();
            var resist = 0.0f;
            if (targets.Any())
            {
                var damageBonus = this.Ability.GetAbilitySpecialData("ethereal_damage_bonus") / 100.0f; // -40
                resist = targets.First().MagicDamageResist + damageBonus;
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, resist);
        }

        protected override float GetRawDamage()
        {
            var damage = this.Ability.GetAbilitySpecialData("blast_damage_base");

            var hero = this.Owner as Hero;
            if (hero != null)
            {
                var multiplier = this.Ability.GetAbilitySpecialData("blast_agility_multiplier"); // 2.0
                if (hero.PrimaryAttribute == Attribute.Strength)
                {
                    damage += multiplier * hero.TotalStrength;
                }
                else if (hero.PrimaryAttribute == Attribute.Agility)
                {
                    damage += multiplier * hero.TotalAgility;
                }
                else if (hero.PrimaryAttribute == Attribute.Intelligence)
                {
                    damage += multiplier * hero.TotalIntelligence;
                }
            }

            return damage;
        }
    }
}