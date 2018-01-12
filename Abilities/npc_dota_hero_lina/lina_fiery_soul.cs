// <copyright file="lina_fiery_soul.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lina
{
    using Ensage.SDK.Abilities.Components;

    public class lina_fiery_soul : PassiveAbility, IHasModifier
    {
        public lina_fiery_soul(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_lina_fiery_soul";
    }
}