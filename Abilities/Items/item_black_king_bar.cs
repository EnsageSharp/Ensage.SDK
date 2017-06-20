// <copyright file="item_black_king_bar.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_black_king_bar : ActiveAbility, IHasModifier
    {
        public item_black_king_bar(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_black_king_bar_immune";
    }
}