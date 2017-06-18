// <copyright file="item_blink.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_blink : RangedAbility
    {
        public item_blink(Item item)
            : base(item)
        {
        }

        public override float CastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blink_range");
            }
        }
    }
}