// <copyright file="slardar_bash.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

using Ensage;
using Ensage.SDK.Abilities;
using Ensage.SDK.Abilities.Components;
using Ensage.SDK.Extensions;

namespace wtf.Sdk.Abilities.npc_dota_hero_necrolyte
{
    public class necrolyte_heartstopper_aura : AuraAbility,IHasModifier, IHasTargetModifier
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
                return this.Ability.GetAbilitySpecialData("aura_damage", this.Ability.Level);
            }
        }
        //计算持续时间内目标单位所受伤害
        public float GetDamage(Unit target, uint duration = 1)
        {
            return target.MaximumHealth * DamagePerSecond/100f * duration;
        }
    }
}