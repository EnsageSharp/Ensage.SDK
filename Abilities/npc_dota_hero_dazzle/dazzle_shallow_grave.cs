// <copyright file="dazzle_shallow_grave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dazzle
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dazzle_shallow_grave : RangedAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public dazzle_shallow_grave(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration_tooltip");
            }
        }

        public float Radius
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return this.Ability.GetAbilitySpecialData("scepter_radius");
                }

                return 0;
            }
        }

        public string TargetModifierName { get; } = "modifier_dazzle_shallow_grave";
    }
}