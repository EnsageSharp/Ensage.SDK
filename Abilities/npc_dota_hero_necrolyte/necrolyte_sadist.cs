// <copyright file="slardar_slithereen_crush.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

using Ensage;
using Ensage.SDK.Abilities;
using Ensage.SDK.Abilities.Components;
using Ensage.SDK.Extensions;

namespace wtf.Sdk.Abilities.npc_dota_hero_necrolyte
{
    public class necrolyte_sadist : AreaOfEffectAbility, IHasTargetModifier,IHasModifier
    {
        public necrolyte_sadist(Ability ability)
            : base(ability)
        {
        }

//        public override float Radius
//        {
//            get
//            {
//                return this.Ability.GetAbilitySpecialData("crush_radius");
//            }
//        }

        public string TargetModifierName { get; } = "modifier_necrolyte_sadist_aura_effect";

        public string ModifierName { get; } = "modifier_necrolyte_sadist_active";
    }
}