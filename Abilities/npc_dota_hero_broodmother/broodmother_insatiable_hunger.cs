// <copyright file="broodmother_insatiable_hunger.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_broodmother
{
    using PlaySharp.Toolkit.Helper.Annotations;

    public class broodmother_insatiable_hunger : ActiveAbility, IHasModifier
    {
        public broodmother_insatiable_hunger([NotNull] Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_broodmother_insatiable_hunger";
    }
}