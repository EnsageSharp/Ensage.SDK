// <copyright file="item_helm_of_the_dominator.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_helm_of_the_dominator : RangedAbility, IAuraAbility
    {
        public item_helm_of_the_dominator(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_helm_of_the_dominator_aura";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }
    }
}