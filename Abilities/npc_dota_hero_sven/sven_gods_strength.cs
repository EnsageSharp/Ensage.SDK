// <copyright file="sven_gods_strength.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_sven
{
    using Ensage.SDK.Abilities.Components;

    public class sven_gods_strength : ActiveAbility, IHasModifier
    {
        public sven_gods_strength(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_sven_gods_strength";
    }
}