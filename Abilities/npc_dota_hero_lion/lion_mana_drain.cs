// <copyright file="lion_mana_drain.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lion
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class lion_mana_drain : RangedAbility, IChannable, IHasTargetModifier
    {
        public lion_mana_drain(Ability ability)
            : base(ability)
        {
        }

        public float ChannelDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
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

        public string TargetModifierName { get; } = "modifier_lion_mana_drain";
    }
}