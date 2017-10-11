// <copyright file="item_abyssal_blade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_abyssal_blade : RangedAbility, IHasTargetModifierTexture
    {
        public item_abyssal_blade(Item item)
            : base(item)
        {
        }

        public string[] TargetModifierTextureName { get; } = { "item_abyssal_blade" };
    }
}