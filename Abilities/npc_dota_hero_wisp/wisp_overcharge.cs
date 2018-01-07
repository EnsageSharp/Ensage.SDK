// <copyright file="wisp_overcharge.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_wisp
{
    using Ensage.SDK.Abilities.Components;

    public class wisp_overcharge : ToggleAbility, IHasModifier, IHasTargetModifier
    {
        public wisp_overcharge(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_wisp_overcharge";

        public string TargetModifierName { get; } = "modifier_wisp_overcharge";
    }
}