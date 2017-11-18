// <copyright file="bristleback_warpath.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bristleback
{
    using Ensage.SDK.Abilities.Components;

    public class bristleback_warpath : PassiveAbility, IHasModifier
    {
        public bristleback_warpath(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_bristleback_warpath_stack";
    }
}