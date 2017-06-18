// <copyright file="item_sheepstick.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_sheepstick : RangedAbility, IHasTargetModifier
    {
        public item_sheepstick(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_sheepstick_debuff";
    }
}