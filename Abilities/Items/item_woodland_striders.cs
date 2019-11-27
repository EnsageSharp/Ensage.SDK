// <copyright file="item_woodland_striders.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_woodland_striders : ActiveAbility, IHasModifier
    {
        public item_woodland_striders(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_woodland_striders_active";
    }
}