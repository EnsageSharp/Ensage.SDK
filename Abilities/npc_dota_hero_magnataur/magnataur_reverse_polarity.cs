// <copyright file="magnataur_reverse_polarity.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_magnataur
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class magnataur_reverse_polarity : ActiveAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public magnataur_reverse_polarity(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("pull_radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_magnataur_reverse_polarity";

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }
    }
}