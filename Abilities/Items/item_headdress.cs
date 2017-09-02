// <copyright file="item_headdress.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_headdress : AuraAbility
    {
        public item_headdress(Item item)
            : base(item)
        {
        }

        public override string AuraModifierName { get; } = "modifier_item_headdress_aura";
    }
}