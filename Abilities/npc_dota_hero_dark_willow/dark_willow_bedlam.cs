// <copyright file="dark_willow_bedlam.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_willow
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dark_willow_bedlam : ActiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public dark_willow_bedlam(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_dark_willow_bedlam";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("attack_radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("attack_damage");
            }
        }
    }
}