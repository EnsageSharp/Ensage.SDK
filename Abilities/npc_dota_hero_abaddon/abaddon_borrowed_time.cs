// <copyright file="abaddon_borrowed_time.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_abaddon
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class abaddon_borrowed_time : ActiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public abaddon_borrowed_time(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                if (!this.IsReady)
                {
                    return false;
                }

                if (this.Owner.IsSilenced())
                {
                    return false;
                }

                if ((Game.RawGameTime - this.LastCastAttempt) < 0.1f)
                {
                    return false;
                }

                return true;
            }
        }

        public string ModifierName { get; } = "modifier_abaddon_borrowed_time";

        public float Radius
        {
            get
            {
                return this.Owner.HasAghanimsScepter() ? this.Ability.GetAbilitySpecialData("redirect_range_tooltip_scepter") : 0;
            }
        }
    }
}