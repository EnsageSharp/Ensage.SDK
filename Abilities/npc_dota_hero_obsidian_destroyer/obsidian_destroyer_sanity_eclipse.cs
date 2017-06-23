// <copyright file="obsidian_destroyer_sanity_eclipse.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class obsidian_destroyer_sanity_eclipse : CircleAbility
    {
        public obsidian_destroyer_sanity_eclipse(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_multiplier");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var amplify = this.Ability.SpellAmplification();
            var multiplier = this.RawDamage;
            var totalDamage = 0.0f;

            foreach (var target in targets)
            {
                var hero = target as Hero;
                if (hero == null)
                {
                    continue;
                }

                var intelligenceDifference = ((Hero)this.Owner).TotalIntelligence - hero.TotalIntelligence;
                if (intelligenceDifference <= 0)
                {
                    continue;
                }

                var reduction = this.Ability.GetDamageReduction(hero, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(intelligenceDifference * multiplier, amplify, reduction);
            }

            return totalDamage;
        }
    }
}