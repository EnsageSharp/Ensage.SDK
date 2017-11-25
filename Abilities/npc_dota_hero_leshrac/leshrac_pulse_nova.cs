// <copyright file="leshrac_pulse_nova.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_leshrac
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class leshrac_pulse_nova : ToggleAbility, IAreaOfEffectAbility, IHasModifier
    {
        public leshrac_pulse_nova(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_leshrac_pulse_nova";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
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