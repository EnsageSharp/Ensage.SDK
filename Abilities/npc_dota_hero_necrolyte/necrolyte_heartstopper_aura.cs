// <copyright file="slardar_bash.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

using Ensage;
using Ensage.SDK.Abilities;
using Ensage.SDK.Abilities.Components;
using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_necrolyte
{
    public class necrolyte_heartstopper_aura : AuraAbility, IHasModifier, IHasTargetModifier
    {
        public necrolyte_heartstopper_aura(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_necrolyte_heartstopper_aura";
        public string TargetModifierName { get; } = "modifier_necrolyte_heartstopper_aura_effect";
        public override string AuraModifierName { get; } = "modifier_necrolyte_heartstopper_aura";
        protected float DamagePerSecond
        {
            get
            {
                return Owner.HasAghanimsScepter() ? this.Ability.GetAbilitySpecialData("aura_damage", this.Ability.Level) * Ability.GetAbilitySpecialData("scepter_multiplier") 
                    : this.Ability.GetAbilitySpecialData("aura_damage", this.Ability.Level);
            }
        }
    }
}