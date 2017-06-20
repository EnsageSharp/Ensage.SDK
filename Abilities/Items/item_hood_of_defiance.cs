// <copyright file="item_hood_of_defiance.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_hood_of_defiance : ActiveAbility, IHasModifier
    {
        public item_hood_of_defiance(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_hood_of_defiance_barrier";
    }
}