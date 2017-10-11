// <copyright file="sven_storm_bolt.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_sven
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class sven_storm_bolt : RangedAbility, IHasTargetModifierTexture, IAreaOfEffectAbility
    {
        public sven_storm_bolt(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bolt_aoe");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bolt_speed");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "sven_storm_bolt" };
    }
}