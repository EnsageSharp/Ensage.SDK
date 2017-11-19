// <copyright file="centaur_hoof_stomp.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_centaur
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class centaur_hoof_stomp : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifierTexture
    {
        public centaur_hoof_stomp(Ability ability)
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

        public string[] TargetModifierTextureName { get; set; } = { "centaur_hoof_stomp" };

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("stomp_damage");
            }
        }
    }
}