// <copyright file="skywrath_mage_arcane_bolt.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using Ensage.SDK.Extensions;

    public class skywrath_mage_arcane_bolt : RangedAbility
    {
        public skywrath_mage_arcane_bolt(Ability ability)
            : base(ability)
        {
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bolt_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("bolt_damage");
                var multiplier = this.Ability.GetAbilitySpecialData("int_multiplier");

                var hero = this.Owner as Hero;
                if (hero != null)
                {
                    damage += hero.TotalIntelligence * multiplier;
                }

                return damage;
            }
        }
    }
}