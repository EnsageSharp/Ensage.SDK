// <copyright file="terrorblade_reflection.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_terrorblade
{
    using System.Linq;

    using Ensage.SDK.Extensions;

    public class terrorblade_reflection : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public terrorblade_reflection(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_terrorblade_reflection_slow";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("range");
            }
        }

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