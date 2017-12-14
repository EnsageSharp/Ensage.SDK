// <copyright file="item_combo_breaker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_aeon_disk : PassiveAbility, IHasModifier
    {
        public item_aeon_disk(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_aeon_disk_buff";
    }
}