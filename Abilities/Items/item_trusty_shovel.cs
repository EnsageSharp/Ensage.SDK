// <copyright file="item_trusty_shovel.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_trusty_shovel : RangedAbility, IChannable
    {
        public item_trusty_shovel(Item item)
            : base(item)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("tree_duration");
            }
        }

        public float ChannelDuration
        {
            get
            {
                return 1; // no special data, rip
            }
        }
        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }
        public float RemainingDuration
        {
            get
            {
                if (!this.IsChanneling)
                {
                    return 0;
                }

                return this.ChannelDuration - this.Ability.ChannelTime;
            }
        }
    }
}