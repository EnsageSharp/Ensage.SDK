// <copyright file="item_butterfly.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_butterfly : ActiveAbility, IHasModifier
    {
        public item_butterfly(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_butterfly_extra";
    }
}