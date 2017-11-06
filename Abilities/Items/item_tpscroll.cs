// <copyright file="item_tpscroll.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_tpscroll : RangedAbility, IChannable, IHasModifier, IHasModifierTexture
    {
        public item_tpscroll(Item item)
            : base(item)
        {
        }

        public float ChannelDuration
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                // sometimes can be longer...
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

        public string[] ModifierTextureName { get; } = { "item_tpscroll" };

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