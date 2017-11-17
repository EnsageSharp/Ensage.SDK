// <copyright file="ancient_apparition_ice_vortex.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ancient_apparition
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class ancient_apparition_ice_vortex : CircleAbility, IHasTargetModifier, IHasDamageAmplifier
    {
        public ancient_apparition_ice_vortex(Ability ability)
            : base(ability)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.Magical;

        public float DamageAmplification
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("spell_resist_pct") / -100.0f;
            }
        }

        public string TargetModifierName { get; } = "modifier_ice_vortex";
    }
}