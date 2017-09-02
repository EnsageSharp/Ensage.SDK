// <copyright file="item_assault.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_assault : AuraAbility
    {
        public item_assault(Item item)
            : base(item)
        {
        }

        public override string AuraModifierName { get; } = "modifier_item_assault_negative_armor";
    }
}