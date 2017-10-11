// <copyright file="item_ghost.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_ghost : ActiveAbility, IHasModifier
    {
        public item_ghost(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_ghost_state";
    }
}