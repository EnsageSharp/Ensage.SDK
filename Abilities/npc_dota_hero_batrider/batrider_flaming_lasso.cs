// <copyright file="batrider_flaming_lasso.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_batrider
{
    using Ensage.SDK.Abilities.Components;

    public class batrider_flaming_lasso : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public batrider_flaming_lasso(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_batrider_flaming_lasso_self";

        public string TargetModifierName { get; } = "modifier_batrider_flaming_lasso";
    }
}