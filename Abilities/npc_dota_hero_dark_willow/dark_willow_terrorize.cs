// <copyright file="dark_willow_terrorize.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_willow
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dark_willow_terrorize : CircleAbility, IHasTargetModifier
    {
        public dark_willow_terrorize(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("initial_delay");
            }
        }

        public string TargetModifierName { get; } = "modifier_dark_willow_debuff_fear";

        protected override string RadiusName { get; } = "destination_radius";

        protected override string SpeedName { get; } = "destination_travel_speed";
    }
}