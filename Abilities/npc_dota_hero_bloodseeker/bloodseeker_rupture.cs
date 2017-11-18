// <copyright file="bloodseeker_rupture.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bloodseeker
{
    using Ensage.SDK.Abilities.Components;

    public class bloodseeker_rupture : RangedAbility, IHasTargetModifier
    {
        public bloodseeker_rupture(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_bloodseeker_rupture";
    }
}