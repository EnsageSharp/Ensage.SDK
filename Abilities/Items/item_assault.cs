// <copyright file="item_assault.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_assault : PassiveAbility, IAuraAbility
    {
        public item_assault(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_assault_negative_armor";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }
    }
}