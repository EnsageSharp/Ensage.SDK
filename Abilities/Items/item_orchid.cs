// <copyright file="item_orchid.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_orchid : RangedAbility, IHasTargetModifier
    {
        public item_orchid(Item item)
            : base(item)
        {
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public string TargetModifierName { get; } = "modifier_orchid_malevolence_debuff";
    }
}