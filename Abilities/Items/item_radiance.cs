// <copyright file="item_radiance.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_radiance : ToggleAbility, IAuraAbility
    {
        public item_radiance(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_radiance_debuff";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }
    }
}