// <copyright file="slardar_slithereen_crush.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_slardar
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class slardar_slithereen_crush : AreaOfEffectAbility, IHasTargetModifier, IHasTargetModifierTexture
    {
        public slardar_slithereen_crush(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("crush_radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_slithereen_crush";

        public string[] TargetModifierTextureName { get; } = { "slardar_slithereen_crush" };
    }
}