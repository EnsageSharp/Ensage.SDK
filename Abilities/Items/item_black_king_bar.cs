﻿// <copyright file="item_black_king_bar.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_black_king_bar : ActiveAbility, IHasModifier
    {
        public item_black_king_bar(Item item)
            : base(item)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.MagicImmune;

        public string ModifierName { get; } = "modifier_black_king_bar_immune";
    }
}