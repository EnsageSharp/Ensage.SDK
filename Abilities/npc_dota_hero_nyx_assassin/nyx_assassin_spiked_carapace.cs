// <copyright file="nyx_assassin_spiked_carapace.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class nyx_assassin_spiked_carapace : ActiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public nyx_assassin_spiked_carapace(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_nyx_assassin_spiked_carapace";

        public float Radius
        {
            get
            {
                var modifier = this.Owner.GetModifierByName("modifier_nyx_assassin_burrow");
                if (modifier != null)
                {
                    return this.Ability.GetAbilitySpecialData("burrow_aoe");
                }

                return 0;
            }
        }
    }
}