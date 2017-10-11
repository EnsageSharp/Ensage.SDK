// <copyright file="item_tango_single.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    internal class item_tango_single : RangedAbility, IHasModifier
    {
        public item_tango_single(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_tango_heal";
    }
}