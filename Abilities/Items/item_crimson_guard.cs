// <copyright file="item_crimson_guard.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_crimson_guard : ActiveAbility, IAreaOfEffectAbility, IHasModifier
    {
        public item_crimson_guard(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_crimson_guard_nostack";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bonus_aoe_radius");
            }
        }
    }
}