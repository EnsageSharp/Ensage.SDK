// <copyright file="TravelBoots.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Aggregation
{
    public abstract class TravelBoots : RangedAbility, IChannable, IHasModifier
    {
        protected TravelBoots(Item item)
            : base(item)
        {
        }

        public float Duration
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                return this.Ability.GetChannelTime(level - 1);
            }
        }

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }

        public string ModifierName { get; } = "modifier_teleporting";

        public float RemainingDuration
        {
            get
            {
                if (!this.IsChanneling)
                {
                    return 0;
                }

                return this.Duration - this.Ability.ChannelTime;
            }
        }
    }
}