// <copyright file="item_orchid.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_orchid : RangedAbility, IHasTargetModifier
    {
        public item_orchid(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_orchid_malevolence_debuff";
    }
}