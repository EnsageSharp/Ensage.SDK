// <copyright file="earth_spirit_geomagnetic_grip.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earth_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class earth_spirit_geomagnetic_grip : LineAbility, IHasTargetModifier
    {
        public earth_spirit_geomagnetic_grip(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Silenced;

        public string TargetModifierName { get; } = "modifier_earth_spirit_geomagnetic_grip_debuff";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rock_damage");
            }
        }
    }
}