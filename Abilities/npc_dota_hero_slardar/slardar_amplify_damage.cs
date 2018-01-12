// <copyright file="slardar_amplify_damage.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_slardar
{
    using Ensage.SDK.Abilities.Components;

    public class slardar_amplify_damage : RangedAbility, IHasTargetModifier
    {
        public slardar_amplify_damage(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_slardar_amplify_damage";
    }
}