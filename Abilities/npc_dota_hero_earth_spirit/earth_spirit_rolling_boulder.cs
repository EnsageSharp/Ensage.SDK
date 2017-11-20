// <copyright file="earth_spirit_rolling_boulder.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earth_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class earth_spirit_rolling_boulder : LineAbility, IHasModifier
    {
        public earth_spirit_rolling_boulder(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("delay");
            }
        }

        public override float CastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("distance");
            }
        }

        public string ModifierName { get; } = "modifier_earth_spirit_rolling_boulder_caster";

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("distance");
            }
        }

        public float RockRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rock_distance");
            }
        }

        public float RockSpeed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rock_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }
    }
}