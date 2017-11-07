// <copyright file="item_hurricane_pike.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_hurricane_pike : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public item_hurricane_pike(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_hurricane_pike_range";

        public float PushLength
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("push_length");
            }
        }

        public float PushSpeed { get; } = 1500.0f;

        public string TargetModifierName { get; } = "modifier_item_hurricane_pike_active";
    }
}