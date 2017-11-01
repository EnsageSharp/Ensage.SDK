// <copyright file="rubick_null_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rubick
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class rubick_null_field : ToggleAbility, IAuraAbility
    {
        public rubick_null_field(Ability ability)
            : base(ability)
        {
        }

        public string AuraModifierName { get; } = "modifier_rubick_null_field_effect";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}