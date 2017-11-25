// <copyright file="bane_enfeeble.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bane
{
    using Ensage.SDK.Abilities.Components;

    public class bane_enfeeble : RangedAbility, IHasTargetModifier
    {
        public bane_enfeeble(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_bane_enfeeble";
    }
}