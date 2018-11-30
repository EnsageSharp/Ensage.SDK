// <copyright file="troll_warlord_berserkers_rage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_troll_warlord
{
    using Ensage.SDK.Abilities.Components;

    public class troll_warlord_berserkers_rage : ToggleAbility, IHasModifier, IHasProcChance, IHasTargetModifier
    {
        public troll_warlord_berserkers_rage(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_troll_warlord_berserkers_rage";
        public string TargetModifierName { get; } = "modifier_troll_warlord_berserkers_rage_ensnare";

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                return Ability.GetAbilitySpecialData("ensnare_chance");
            }
        }
    }
}