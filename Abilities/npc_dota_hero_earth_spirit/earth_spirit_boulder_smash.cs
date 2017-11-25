// <copyright file="earth_spirit_boulder_smash.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earth_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class earth_spirit_boulder_smash : LineAbility, IHasTargetModifierTexture
    {
        public earth_spirit_boulder_smash(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float CastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rock_search_aoe");
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rock_distance");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "earth_spirit_boulder_smash" };

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rock_damage");
            }
        }
    }
}