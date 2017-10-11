// <copyright file="item_bottle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.Items;
    using Ensage.SDK.Abilities.Components;

    public class item_bottle : RangedAbility, IHasTargetModifier
    {
        public item_bottle(Item item)
            : base(item)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Item.CurrentCharges > 0 && this.StoredRune == RuneType.None && base.CanBeCasted;
            }
        }

        public RuneType StoredRune
        {
            get
            {
                return ((Bottle)this.Item).StoredRune;
            }
        }

        public string TargetModifierName { get; } = "modifier_bottle_regeneration";
    }
}