// <copyright file="dark_seer_vacuum.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_seer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dark_seer_vacuum : CircleAbility, IHasTargetModifier
    {
        public dark_seer_vacuum(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_dark_seer_vacuum";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}