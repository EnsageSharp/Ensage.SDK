// <copyright file="troll_warlord_berserkers_rage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_troll_warlord
{
    using Ensage.SDK.Abilities.Components;

    public class troll_warlord_berserkers_rage : ToggleAbility, IHasModifier
    {
        public troll_warlord_berserkers_rage(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_troll_warlord_berserkers_rage";
    }
}