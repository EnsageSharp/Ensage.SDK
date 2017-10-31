// <copyright file="item_headdress.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_headdress : PassiveAbility, IAuraAbility
    {
        public item_headdress(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_headdress_aura";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }
    }
}