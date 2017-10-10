// <copyright file="item_ethereal_blade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using PlaySharp.Toolkit.Helper.Annotations;

    [PublicAPI]
    public class item_ethereal_blade : RangedAbility, IHasTargetModifier, IHasDamageAmplifier
    {
        public item_ethereal_blade(Item item)
            : base(item)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.Magical;

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_item_ethereal_blade_ethereal";

        public float Value
        {
            get
            {
                return -this.Ability.GetAbilitySpecialData("ethereal_damage_bonus") / 100.0f;
            }
        }

        protected override float RawDamage
        {
            get
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

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;

            var amplify = this.Owner.GetSpellAmplification();
            if (!targets.Any())
            {
                return DamageHelpers.GetSpellDamage(damage, amplify);
            }

            var damageBonus = -this.Ability.GetAbilitySpecialData("ethereal_damage_bonus") / 100.0f; // -40 => 0.4
            var resist = this.Ability.GetDamageReduction(targets.First(), this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, -resist, damageBonus);
        }
    }
}