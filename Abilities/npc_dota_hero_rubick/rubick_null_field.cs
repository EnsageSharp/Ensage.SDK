// <copyright file="rubick_null_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rubick
{
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

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                // toggleable ability, but needs UseAbility() to toggle
                if (!this.CanBeCasted)
                {
                    return;
                }

                var result = false;
                if (value)
                {
                    if (!this.Enabled)
                    {
                        result = this.Ability.UseAbility();
                    }
                }
                else
                {
                    if (this.Enabled)
                    {
                        result = this.Ability.UseAbility();
                    }
                }

                if (result)
                {
                    this.LastCastAttempt = Game.RawGameTime;
                }
            }
        }
    }
}