// <copyright file="legion_commander_overwhelming_odds.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_legion_commander
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class legion_commander_overwhelming_odds : CircleAbility, IHasModifier
    {
        public legion_commander_overwhelming_odds(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_legion_commander_overwhelming_odds";

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
            var amplify = this.Ability.SpellAmplification();

            if (!targets.Any())
            {
                return damage;
            }

            var totalDamage = 0.0f;

            foreach (var target in targets)
            {
                var hero = target is Hero;
                if (hero)
                {
                    damage += this.Ability.GetAbilitySpecialData("damage_per_hero");
                }

                var creep = target is Creep;
                if (target.IsNeutral || creep)
                {
                    damage += this.Ability.GetAbilitySpecialData("damage_per_unit");
                }
            }

            var reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);

            return totalDamage;
        }
    }
}