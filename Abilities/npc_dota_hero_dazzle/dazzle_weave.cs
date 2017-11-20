// <copyright file="dazzle_weave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dazzle
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dazzle_weave : CircleAbility, IHasTargetModifier
    {
        public dazzle_weave(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_dazzle_weave_armor";
    }
}