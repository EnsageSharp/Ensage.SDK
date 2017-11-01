// <copyright file="item_nullifier.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    class item_nullifier : RangedAbility, IHasTargetModifier
    {
        public item_nullifier(Item item)
            : base(item)
        {
        }

        public override float Speed
        {
            get
            {
                return Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_nullifier_mute";

    }
}
