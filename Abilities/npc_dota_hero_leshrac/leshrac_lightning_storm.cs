// <copyright file="leshrac_lightning_storm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_leshrac
{
    using Ensage.SDK.Abilities.Components;

    public class leshrac_lightning_storm : AreaOfEffectAbility, IHasTargetModifier
    {
        public leshrac_lightning_storm(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_leshrac_lightning_storm_slow";
    }
}