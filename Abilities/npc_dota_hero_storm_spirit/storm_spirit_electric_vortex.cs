// <copyright file="storm_spirit_electric_vortex.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_storm_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class storm_spirit_electric_vortex : RangedAbility, IHasTargetModifier, IHasModifier, IAreaOfEffectAbility
    {
        public storm_spirit_electric_vortex(Ability ability)
            : base(ability)
        {
        }

        public override float CastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return this.Ability.GetAbilitySpecialData("radius_scepter");
                }

                return base.CastRange;
            }
        }

        public string ModifierName { get; } = "modifier_storm_spirit_electric_vortex_self_slow";

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

        public string TargetModifierName { get; } = "modifier_storm_spirit_electric_vortex_pull";
    }
}