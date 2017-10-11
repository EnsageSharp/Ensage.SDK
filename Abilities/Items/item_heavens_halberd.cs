// <copyright file="item_heavens_halberd.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_heavens_halberd : RangedAbility, IHasTargetModifier
    {
        public item_heavens_halberd(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_heavens_halberd_debuff";
    }
}