// <copyright file="rubick_null_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.npc_dota_hero_rubick
{
    using Ensage.SDK.Extensions;

    public class rubick_arcane_supremacy : PassiveAbility, IHasModifier
    {
        public rubick_arcane_supremacy(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_rubick_arcane_supremacy";
    }
}