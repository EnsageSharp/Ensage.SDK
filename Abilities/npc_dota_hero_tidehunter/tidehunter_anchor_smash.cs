// <copyright file="tidehunter_anchor_smash.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_tidehunter
{
    using Ensage.SDK.Abilities.Components;

    public class tidehunter_anchor_smash : AreaOfEffectAbility, IHasTargetModifier
    {
        public tidehunter_anchor_smash(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_tidehunter_anchor_smash";
    }
}