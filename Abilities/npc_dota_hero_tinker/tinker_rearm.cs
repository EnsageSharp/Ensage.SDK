// <copyright file="tinker_rearm.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_tinker
{
    using Ensage.SDK.Abilities.Components;
    public class tinker_rearm : ActiveAbility, IHasModifier
    {
        public tinker_rearm(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_tinker_rearm";
    }
}
