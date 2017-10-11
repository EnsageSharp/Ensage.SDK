// <copyright file="rubick_telekinesis.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rubick
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class rubick_telekinesis : RangedAbility, IHasTargetModifier, IHasTargetModifierTexture
    {
        public rubick_telekinesis(Ability ability)
            : base(ability)
        {
            var landAbility = this.Owner.GetAbilityById(AbilityId.rubick_telekinesis_land);
            this.LandAbility = new rubick_telekinesis_land(landAbility);
        }

        public rubick_telekinesis_land LandAbility { get; set; }

        public string TargetModifierName { get; } = "modifier_rubick_telekinesis";

        public string[] TargetModifierTextureName { get; } = { "rubick_telekinesis" };
    }
}