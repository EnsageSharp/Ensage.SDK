// <copyright file="item_diffusal_blade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_diffusal_blade : RangedAbility, IHasTargetModifier
    {
        public item_diffusal_blade(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_item_diffusal_blade_slow";
    }
}