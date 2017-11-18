// <copyright file="phantom_assassin_blur.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_phantom_assassin
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class phantom_assassin_blur : PassiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public phantom_assassin_blur(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_phantom_assassin_blur_active";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}