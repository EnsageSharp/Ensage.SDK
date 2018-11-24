// <copyright file="obsidian_destroyer_essence_aura.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using Ensage.SDK.Extensions;

    public class obsidian_destroyer_equilibrium : ActiveAbility, IHasModifier, IHasTargetModifier
    {
        public obsidian_destroyer_equilibrium(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_obsidian_destroyer_equilibrium";
        public string TargetModifierName { get; } = "modifier_obsidian_destroyer_equilibrium_debuff";
    }
}