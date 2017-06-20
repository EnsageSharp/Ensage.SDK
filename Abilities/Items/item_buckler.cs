// <copyright file="item_buckler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_buckler : ActiveAbility, IAreaOfEffectAbility, IHasModifier
    {
        public item_buckler(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_buckler_effect";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bonus_aoe_radius");
            }
        }
    }
}