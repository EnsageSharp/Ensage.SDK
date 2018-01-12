// <copyright file="tidehunter_kraken_shell.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_tidehunter
{
    using Ensage.SDK.Abilities.Components;

    public class tidehunter_kraken_shell : PassiveAbility, IHasModifier
    {
        public tidehunter_kraken_shell(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_tidehunter_kraken_shell";
    }
}