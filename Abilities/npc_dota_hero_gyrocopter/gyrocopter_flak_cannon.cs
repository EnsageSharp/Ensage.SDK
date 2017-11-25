// <copyright file="gyrocopter_flak_cannon.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_gyrocopter
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class gyrocopter_flak_cannon : ActiveAbility, IAreaOfEffectAbility, IHasModifier
    {
        public gyrocopter_flak_cannon(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_gyrocopter_flak_cannon";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }
    }
}