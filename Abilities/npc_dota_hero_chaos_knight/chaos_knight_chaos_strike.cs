// <copyright file="chaos_knight_chaos_strike.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chaos_knight
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chaos_knight_chaos_strike : PassiveAbility, IHasCritChance
    {
        public chaos_knight_chaos_strike(Ability ability)
            : base(ability)
        {
        }

        public float CritMultiplier
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("crit_damage") / 100f;
            }
        }

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("crit_chance") / 100f;
            }
        }
    }
}