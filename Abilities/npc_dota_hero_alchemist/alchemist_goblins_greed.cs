// <copyright file="alchemist_goblins_greed.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_alchemist
{
    using Ensage.SDK.Extensions;

    public class alchemist_goblins_greed : PassiveAbility
    {
        public alchemist_goblins_greed(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }
    }
}