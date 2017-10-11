// <copyright file="item_mask_of_madness.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_mask_of_madness : ActiveAbility, IHasModifier
    {
        public item_mask_of_madness(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_mask_of_madness_berserk";
    }
}