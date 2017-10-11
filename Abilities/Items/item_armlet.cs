// <copyright file="item_armlet.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_armlet : ToggleAbility, IHasModifier
    {
        public item_armlet(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_armlet_unholy_strength";
    }
}