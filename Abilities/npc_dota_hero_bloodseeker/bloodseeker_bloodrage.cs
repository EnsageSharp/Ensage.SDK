// <copyright file="bloodseeker_bloodrage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bloodseeker
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class bloodseeker_bloodrage : RangedAbility, IHasDamageAmplifier, IHasTargetModifier
    {
        public bloodseeker_bloodrage(Ability ability)
            : base(ability)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public float DamageAmplification
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_increase_pct") / 100.0f;
            }
        }

        public string TargetModifierName { get; } = "modifier_bloodseeker_bloodrage";
    }
}