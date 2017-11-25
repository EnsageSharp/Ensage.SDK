// <copyright file="huskar_inner_vitality.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class huskar_inner_vitality : RangedAbility, IHasTargetModifier, IHasHealthRestore
    {
        public huskar_inner_vitality(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_huskar_inner_vitality";

        public float TotalHealthRestore
        {
            get
            {
                var heal = this.Ability.GetAbilitySpecialData("heal");
                var percentage = this.Ability.GetAbilitySpecialData("tooltip_attrib_bonus") / 100f;

                return (heal + (((this.Owner as Hero)?.TotalAgility ?? 0) * percentage)) * this.Duration;
            }
        }
    }
}