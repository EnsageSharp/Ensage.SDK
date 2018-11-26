// <copyright file="brewmaster_drunken_brawler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;
using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_brewmaster
{
    public class brewmaster_drunken_brawler : ActiveAbility, IHasModifier, IHasCritChance
    {
        public brewmaster_drunken_brawler(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_brewmaster_drunken_brawler";

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                return Ability.GetAbilitySpecialData("crit_chance");
            }
        }

        public float CritMultiplier
        {
            get
            {
                return Ability.GetAbilitySpecialDataWithTalent(Owner, "crit_multiplier");
            }
        }
    }
}