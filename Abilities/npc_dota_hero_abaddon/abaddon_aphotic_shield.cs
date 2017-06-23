// <copyright file="abaddon_aphotic_shield.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_abaddon
{
    using Ensage.SDK.Extensions;

    public class abaddon_aphotic_shield : RangedAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public abaddon_aphotic_shield(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_abaddon_aphotic_shield";
    }
}