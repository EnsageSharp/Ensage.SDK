// <copyright file="item_clarity.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_clarity : RangedAbility, IHasTargetModifier
    {
        public item_clarity(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_clarity_potion";
    }
}