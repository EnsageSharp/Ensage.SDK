// <copyright file="item_diffusal_blade_2.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_diffusal_blade_2 : ActiveAbility, IHasTargetModifier
    {
        public item_diffusal_blade_2(Item item)
            : base(item)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Item.CurrentCharges > 0 && base.CanBeCasted;
            }
        }

        public string TargetModifierName { get; } = "modifier_item_diffusal_blade_slow";
    }
}