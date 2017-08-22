// <copyright file="broodmother_incapacitating_bite.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_broodmother
{
    using PlaySharp.Toolkit.Helper.Annotations;

    public class broodmother_incapacitating_bite : PassiveAbility, IHasTargetModifier
    {
        public broodmother_incapacitating_bite([NotNull] Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_broodmother_incapacitating_bite_orb";
    }
}