// <copyright file="crystal_maiden_brilliance_aura.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_crystal_maiden
{
    public class crystal_maiden_brilliance_aura : AuraAbility
    {
        public crystal_maiden_brilliance_aura(Ability ability)
            : base(ability)
        {
        }

        public override string AuraModifierName { get; } = "modifier_crystal_maiden_brilliance_aura_effect";

        public override float AuraRadius { get; } = float.MaxValue;
    }
}