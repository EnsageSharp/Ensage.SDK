// <copyright file="dazzle_weave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dazzle
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dazzle_bad_juju : PassiveAbility, IHasTargetModifier
    {
        public dazzle_bad_juju(Ability ability)
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

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_dazzle_bad_juju_armor";
    }
}