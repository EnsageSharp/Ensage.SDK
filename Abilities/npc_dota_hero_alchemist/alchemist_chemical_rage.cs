// <copyright file="alchemist_chemical_rage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_alchemist
{
    using Ensage.SDK.Abilities.Components;

    public class alchemist_chemical_rage : ActiveAbility, IHasModifier
    {
        public alchemist_chemical_rage(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_alchemist_chemical_rage";
    }
}