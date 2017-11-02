// <copyright file="item_combo_breaker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    // C-C-C-COMBO BREAKER
    public class item_combo_breaker : PassiveAbility, IHasModifier
    {
        public item_combo_breaker(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_combo_breaker_buff";
    }
}