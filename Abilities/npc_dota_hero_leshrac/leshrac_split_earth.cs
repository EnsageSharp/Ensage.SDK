// <copyright file="leshrac_split_earth.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_leshrac
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class leshrac_split_earth : CircleAbility, IHasTargetModifierTexture
    {
        public leshrac_split_earth(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("delay");
            }
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "leshrac_split_earth" };
    }
}