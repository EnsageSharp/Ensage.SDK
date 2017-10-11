// <copyright file="item_solar_crest.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_solar_crest : RangedAbility, IHasTargetModifier
    {
        public item_solar_crest(Item item)
            : base(item)
        {
        }

        /// <summary>
        ///     "modifier_item_solar_crest_armor_addition" when casted on allies.
        /// </summary>
        public string TargetModifierName { get; } = "modifier_item_solar_crest_armor_reduction";
    }
}