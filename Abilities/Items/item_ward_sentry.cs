// <copyright file="item_ward_sentry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_ward_sentry : RangedAbility
    {
        public item_ward_sentry(Item item)
            : base(item)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.ProvidesVision;
    }
}