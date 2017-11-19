// <copyright file="item_rod_of_atos.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_rod_of_atos : RangedAbility, IHasTargetModifier
    {
        public item_rod_of_atos(Item item)
            : base(item)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public override float Speed { get; } = 1500f; // no speed value in special data

        public string TargetModifierName { get; } = "modifier_rod_of_atos_debuff";
    }
}