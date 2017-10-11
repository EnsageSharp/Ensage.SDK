// <copyright file="nyx_assassin_spiked_carapace.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using Ensage.SDK.Abilities.Components;

    public class nyx_assassin_spiked_carapace : ActiveAbility, IHasModifier
    {
        public nyx_assassin_spiked_carapace(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_nyx_assassin_spiked_carapace";
    }
}