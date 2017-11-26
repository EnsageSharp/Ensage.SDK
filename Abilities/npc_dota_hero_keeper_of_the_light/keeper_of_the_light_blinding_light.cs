// <copyright file="keeper_of_the_light_blinding_light.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Abilities.Components;

    public class keeper_of_the_light_blinding_light : CircleAbility, IHasTargetModifier
    {
        public keeper_of_the_light_blinding_light(Ability ability)
            : base(ability)
        {
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && this.Ability.IsActivated;
            }
        }

        public string TargetModifierName { get; } = "modifier_keeper_of_the_light_blinding_light";
    }
}