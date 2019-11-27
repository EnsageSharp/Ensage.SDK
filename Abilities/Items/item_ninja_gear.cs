// <copyright file="item_ninja_gear.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_ninja_gear : ActiveAbility, IHasModifier
    {
        public item_ninja_gear(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_smoke_of_deceit";
    }
}