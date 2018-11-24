// <copyright file="dark_seer_wall_of_replica.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_seer
{
    using Ensage.SDK.Extensions;

    public class dark_seer_wall_of_replica : LineAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        // todo: Add vector targeting stuff.
        public dark_seer_wall_of_replica(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("width");
            }
        }

        public string TargetModifierName { get; } = "modifier_dark_seer_wall_slow";
    }
}