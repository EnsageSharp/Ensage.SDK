// <copyright file="jakiro_ice_path.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_jakiro
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class jakiro_ice_path : LineAbility, IHasTargetModifier
    {
        public jakiro_ice_path(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("path_delay");
            }
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "duration");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("path_radius");
            }
        }

        public string TargetModifierName { get; set; }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}