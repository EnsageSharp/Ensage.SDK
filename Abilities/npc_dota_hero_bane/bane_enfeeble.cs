// <copyright file="bane_enfeeble.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bane
{
    using Ensage.SDK.Abilities.Components;

    public class bane_enfeeble : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public bane_enfeeble(Ability ability)
            : base(ability)
        {
        }

        // ModifierName comes with a talent and share the same name as target modifier. Added it to avoid confusion.
        public string TargetModifierName { get; } = "modifier_bane_enfeeble";
        public string ModifierName { get; } = "modifier_bane_enfeeble";
    }
}