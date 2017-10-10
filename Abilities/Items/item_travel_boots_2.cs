// <copyright file="item_travel_boots_2.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Aggregation;

    public class item_travel_boots_2 : TravelBoots, IHasModifierTexture
    {
        public item_travel_boots_2(Item item)
            : base(item)
        {
        }
        public string[] ModifierTextureName { get; } = { "item_travel_boots_2" };
    }
}
