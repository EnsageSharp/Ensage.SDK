// <copyright file="clinkz_death_pact.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_clinkz
{
    using Ensage.SDK.Abilities.Components;

    public class clinkz_death_pact : RangedAbility, IHasModifier
    {
        public clinkz_death_pact(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_clinkz_death_pact";
    }
}