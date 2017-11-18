// <copyright file="arc_warden_magnetic_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_arc_warden
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class arc_warden_magnetic_field : CircleAbility, IHasModifier
    {
        public arc_warden_magnetic_field(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string ModifierName { get; } = "modifier_arc_warden_magnetic_field_evasion";
    }
}