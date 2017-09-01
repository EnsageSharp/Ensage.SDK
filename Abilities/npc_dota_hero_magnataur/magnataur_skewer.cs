// <copyright file="magnataur_skewer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_magnataur
{
    using Ensage.SDK.Extensions;

    public class magnataur_skewer : LineAbility, IHasTargetModifier, IHasModifier
    {
        public magnataur_skewer(Ability ability)
            : base(ability)
        {
        }

        protected override string RadiusName { get; } = "skewer_radius";

        protected override string SpeedName { get; } = "skewer_speed";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("skewer_damage");
            }
        }

        public string ModifierName { get; } = "modifier_magnataur_skewer_movement";

        public string TargetModifierName { get; } = "modifier_magnataur_skewer_impact";
    }
}