// <copyright file="night_stalker_crippling_fear.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_night_stalker
{
    using Ensage.SDK.Abilities.Components;

    public class night_stalker_crippling_fear : ActiveAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public night_stalker_crippling_fear(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_night_stalker_crippling_fear_aura";

        public float Radius
        {
            get
            {
                return Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}