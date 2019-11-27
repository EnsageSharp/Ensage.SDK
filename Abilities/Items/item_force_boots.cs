// <copyright file="item_force_boots.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_force_boots : ActiveAbility, IHasModifier
    {
        public item_force_boots(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_force_boots_active";

        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("push_duration");
            }
        }

        public float PushLength
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("push_length");
            }
        }
    }
}