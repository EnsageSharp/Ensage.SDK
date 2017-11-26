// <copyright file="kunkka_ghostship.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_kunkka
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class kunkka_ghostship : CircleAbility, IHasModifier, IHasTargetModifierTexture
    {
        public kunkka_ghostship(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                var delay = this.Ability.GetAbilitySpecialData("tooltip_delay");
                if (this.Owner.HasAghanimsScepter())
                {
                    delay /= 2f;
                }

                return delay;
            }
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public bool IsGhostSheepFleetActive
        {
            get
            {
                return this.Owner.GetAbilityById(AbilityId.special_bonus_unique_kunkka_3)?.Level > 0;
            }
        }

        public string ModifierName { get; } = "modifier_kunkka_ghost_ship_damage_absorb";

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("ghostship_width");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "kunkka_ghostship" };
    }
}