// <copyright file="item_repair_kit.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

using System.Linq;

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_repair_kit : RangedAbility, IHasTargetModifier
    {
        public item_repair_kit(Item item)
            : base(item)
        {
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x is Building);
        }

        public string TargetModifierName { get; } = "modifier_repair_kit";
    }
}