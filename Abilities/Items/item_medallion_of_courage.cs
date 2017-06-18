// <copyright file="item_medallion_of_courage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_medallion_of_courage : RangedAbility, IHasTargetModifier
    {
        public item_medallion_of_courage(Item item)
            : base(item)
        {
        }

        /// <summary>
        ///  "modifier_item_medallion_of_courage_armor_addition" when casted on allies
        /// </summary>
        public string TargetModifierName { get; } = "modifier_item_medallion_of_courage_armor_reduction";
    }
}