// <copyright file="vengefulspirit_wave_of_terror.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    public class vengefulspirit_wave_of_terror : LineAbility
    {
        public vengefulspirit_wave_of_terror(Ability ability)
            : base(ability)
        {
        }

        protected override string RadiusName { get; } = "wave_width";

        // public override bool HasAreaOfEffect { get; } = true; // TODO: wait for aoe bik

        protected override string SpeedName { get; } = "wave_speed";

        public override float GetDamage(params Unit[] targets)
        {
            // TODO
            return base.GetDamage(targets);
        }
    }
}