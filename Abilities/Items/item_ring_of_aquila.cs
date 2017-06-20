// <copyright file="item_ring_of_aquila.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_ring_of_aquila : ToggleAbility, IAuraAbility
    {
        public item_ring_of_aquila(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_ring_of_aquila_aura_bonus";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }
    }
}