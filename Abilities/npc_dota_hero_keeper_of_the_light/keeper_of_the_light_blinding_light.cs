// <copyright file="keeper_of_the_light_blinding_light.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Abilities.Components;

    public class keeper_of_the_light_blinding_light : CircleAbility, IHasTargetModifier
    {
        public keeper_of_the_light_blinding_light(Ability ability)
            : base(ability)
        {
        }

        public float KnocbackDuration
        {
            get
            {
                return Ability.GetAbilitySpecialData("knockback_duration");
            }
        }

        public float KnocbackDistance
        {
            get
            {
                return Ability.GetAbilitySpecialData("knockback_distance");
            }
        }

        public string TargetModifierName { get; } = "modifier_keeper_of_the_light_blinding_light";
    }
}