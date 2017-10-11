// <copyright file="terrorblade_metamorphosis.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_terrorblade
{
    using Ensage.SDK.Abilities.Components;

    public class terrorblade_metamorphosis : ActiveAbility, IHasModifier
    {
        public terrorblade_metamorphosis(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_terrorblade_metamorphosis";
    }
}