// <copyright file="faceless_void_time_lock.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_faceless_void
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class faceless_void_time_lock : PassiveAbility, IHasProcChance
    {
        public faceless_void_time_lock(Ability ability)
            : base(ability)
        {
        }

        public float BonusDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "bonus_damage");
            }
        }

        public bool IsPseudoChance { get; } = true;

        public float ProcChance
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("chance_pct") / 100f;
            }
        }
    }
}