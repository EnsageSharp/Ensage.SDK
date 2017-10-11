// <copyright file="undying_flesh_golem.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_undying
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class undying_flesh_golem : ActiveAbility, IAuraAbility, IHasModifier
    {
        public undying_flesh_golem(Ability ability)
            : base(ability)
        {
        }

        public string AuraModifierName { get; } = "modifier_undying_flesh_golem_plague_aura";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string ModifierName { get; } = "modifier_undying_flesh_golem";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) <= (this.CastRange + this.Radius));
        }
    }
}