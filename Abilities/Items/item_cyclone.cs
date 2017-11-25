// <copyright file="item_cyclone.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_cyclone : RangedAbility, IHasTargetModifier
    {
        public item_cyclone(Item item)
            : base(item)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public string TargetModifierName { get; } = "modifier_eul_cyclone";
    }
}