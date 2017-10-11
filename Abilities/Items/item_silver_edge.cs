// <copyright file="item_silver_edge.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_silver_edge : ActiveAbility, IHasModifier, IHasTargetModifier
    {
        public item_silver_edge(Item item)
            : base(item)
        {
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Physical;
            }
        }

        public string ModifierName { get; } = "modifier_item_silver_edge_windwalk";

        public string TargetModifierName { get; } = "modifier_silver_edge_debuff";
    }
}