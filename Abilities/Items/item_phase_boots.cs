// <copyright file="item_phase_boots.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_phase_boots : ActiveAbility, IHasModifier
    {
        public item_phase_boots(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_phase_boots_active";
    }
}