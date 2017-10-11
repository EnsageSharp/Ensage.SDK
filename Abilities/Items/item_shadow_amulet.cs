// <copyright file="item_shadow_amulet.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_shadow_amulet : RangedAbility, IHasTargetModifier
    {
        public item_shadow_amulet(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_item_shadow_amulet_fade";
    }
}