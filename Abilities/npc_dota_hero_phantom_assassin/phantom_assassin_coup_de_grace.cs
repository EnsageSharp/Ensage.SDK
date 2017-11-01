// <copyright file="phantom_assassin_coup_de_grace.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_phantom_assassin
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class phantom_assassin_coup_de_grace : PassiveAbility, IHasCritChance
    {
        public phantom_assassin_coup_de_grace(Ability ability)
            : base(ability)
        {
        }

        public float CritMultiplier
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("crit_bonus") / 100.0f;
            }
        }

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                var chance = this.Ability.GetAbilitySpecialData("crit_chance");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_phantom_assassin_2);
                if (talent?.Level > 0)
                {
                    chance += talent.GetAbilitySpecialData("value");
                }

                return chance / 100.0f;
            }
        }
    }
}