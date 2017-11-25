// <copyright file="bristleback_viscous_nasal_goo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bristleback
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class bristleback_viscous_nasal_goo : RangedAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public bristleback_viscous_nasal_goo(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return this.Ability.GetAbilitySpecialData("radius_scepter");
                }

                return 0;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("goo_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_bristleback_viscous_nasal_goo";
    }
}