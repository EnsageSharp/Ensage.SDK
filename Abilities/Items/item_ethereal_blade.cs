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
            if (!targets.Any())
            {
                return DamageHelpers.GetSpellDamage(damage, amplify);
            }

            var damageBonus = -this.Ability.GetAbilitySpecialData("ethereal_damage_bonus") / 100.0f; // -40 => 0.4
            var resist = this.Ability.GetDamageReduction(targets.First()); 

            return DamageHelpers.GetSpellDamage(damage, amplify, -resist, damageBonus);
        }

        protected override float GetRawDamage()
        {
            var damage = this.Ability.GetAbilitySpecialData("blast_damage_base");

            var hero = this.Owner as Hero;
            if (hero != null)
            {
                var multiplier = this.Ability.GetAbilitySpecialData("blast_agility_multiplier"); // 2.0
                switch (hero.PrimaryAttribute)
                {
                    case Attribute.Strength:
                        damage += multiplier * hero.TotalStrength;
                        break;
                    case Attribute.Agility:
                        damage += multiplier * hero.TotalAgility;
                        break;
                    case Attribute.Intelligence:
                        damage += multiplier * hero.TotalIntelligence;
                        break;
                }
            }

            return damage;
        }
    }
}