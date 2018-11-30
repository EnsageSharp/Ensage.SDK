// <copyright file="pangolier_heartpiercer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System;

namespace Ensage.SDK.Abilities.npc_dota_hero_pangolier
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class pangolier_lucky_shot : PassiveAbility, IHasTargetModifier, IHasProcChance
    {
        public pangolier_lucky_shot(Ability ability)
            : base(ability)
        {
        }

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("chance_pct");
            }
        }

        // modifier_pangolier_luckyshot_silence
        // modifier_pangolier_luckyshot_disarm
        // I don't know how it should be handled. Didn't want to turn TargetModifierName to string array for 1 spell.
        public string TargetModifierName { get; } = "modifier_pangolier_luckyshot_";
    }
}