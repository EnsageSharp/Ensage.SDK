// <copyright file="disruptor_kinetic_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_disruptor
{
    using Ensage.SDK.Extensions;

    public class disruptor_kinetic_field : CircleAbility
    {
        public disruptor_kinetic_field(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("formation_time");
            }
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "duration");
            }
        }
    }
}