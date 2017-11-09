// <copyright file="pangolier_heartpiercer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pangolier
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class pangolier_heartpiercer : PassiveAbility, IHasTargetModifier, IHasProcChance
    {
        public pangolier_heartpiercer(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("debuff_delay");
            }
        }

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("chance_pct");
            }
        }

        //modifier_pangolier_heartpiercer_delay
        public string TargetModifierName { get; } = "modifier_pangolier_heartpiercer_debuff";
    }
}