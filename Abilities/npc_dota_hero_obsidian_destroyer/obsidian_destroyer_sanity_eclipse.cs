// <copyright file="obsidian_destroyer_sanity_eclipse.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class obsidian_destroyer_sanity_eclipse : CircleAbility, IHasModifier
    {
        public obsidian_destroyer_sanity_eclipse(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("base_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var amplify = this.Ability.SpellAmplification();
            var rawDamage = this.RawDamage;
            var multiplier = Ability.GetAbilitySpecialDataWithTalent(Owner, "damage_multiplier");
            var totalDamage = 0.0f;

            foreach (var target in targets)
            {
                var hero = target as Hero;
                if (hero == null)
                {
                    continue;
                }

                var manaDifference = ((Hero)this.Owner).MaximumMana - hero.MaximumMana;
                if (manaDifference <= 0)
                {
                    continue;
                }

                var reduction = this.Ability.GetDamageReduction(hero, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage((manaDifference + rawDamage) * multiplier, amplify, reduction);
            }

            return totalDamage;
        }

        public string ModifierName { get; } = "modifier_obsidian_destroyer_sanity_eclipse_charge";
    }
}