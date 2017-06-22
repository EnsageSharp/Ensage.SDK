// <copyright file="DiffusalBlade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class DiffusalBlade : RangedAbility, IHasTargetModifier
    {
        protected DiffusalBlade(Item item)
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