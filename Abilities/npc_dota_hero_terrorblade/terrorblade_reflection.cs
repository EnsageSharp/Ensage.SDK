// <copyright file="terrorblade_reflection.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_terrorblade
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class terrorblade_reflection : CircleAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public terrorblade_reflection(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("range");
            }
        }

        public string TargetModifierName { get; } = "modifier_terrorblade_reflection_slow";

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }
    }
}