// <copyright file="keeper_of_the_light_blinding_light.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Abilities.Components;

    public class keeper_of_the_light_will_o_wisp : CircleAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public keeper_of_the_light_will_o_wisp(Ability ability)
            : base(ability)
        {
        }

        public float FlickerCount
        {
            get
            {
                return Ability.GetAbilitySpecialDataWithTalent(Owner, "on_count");
            }
        }

        public override float Radius
        {
            get
            {
                return Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_keeper_of_the_light_will_o_wisp";
    }
}