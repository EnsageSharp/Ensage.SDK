// <copyright file="gyrocopter_homing_missile.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_gyrocopter
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class gyrocopter_homing_missile : RangedAbility, IHasTargetModifierTexture
    {
        public gyrocopter_homing_missile(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("pre_flight_time");
            }
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "gyrocopter_homing_missile" };
    }
}