// <copyright file="searing_chains.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ember_spirit
{
    using System.Linq;

    using Ensage.SDK.Extensions;

    public class ember_spirit_searing_chains : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public ember_spirit_searing_chains(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_ember_spirit_searing_chains";

        public override bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            return this.Owner.Distance2D(targets.First()) < this.Radius;
        }
    }
}