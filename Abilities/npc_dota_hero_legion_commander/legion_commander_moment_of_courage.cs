// <copyright file="legion_commander_moment_of_courage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_legion_commander
{
    using Ensage.SDK.Abilities.Components;

    public class legion_commander_moment_of_courage : PassiveAbility, IHasModifier
    {
        public legion_commander_moment_of_courage(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_legion_commander_moment_of_courage_lifesteal";
    }
}