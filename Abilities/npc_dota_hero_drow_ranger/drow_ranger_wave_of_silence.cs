// <copyright file="drow_ranger_wave_of_silence.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_drow_ranger
{
    using Ensage.SDK.Abilities.Components;

    public class drow_ranger_wave_of_silence : LineAbility, IHasTargetModifier
    {
        public drow_ranger_wave_of_silence(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_drowranger_wave_of_silence_knockback";

        protected override string RadiusName { get; } = "wave_width";

        protected override string SpeedName { get; } = "wave_speed";
    }
}