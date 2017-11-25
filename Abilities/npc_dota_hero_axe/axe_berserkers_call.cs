// <copyright file="axe_berserkers_call.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_axe
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class axe_berserkers_call : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier, IHasModifier
    {
        public axe_berserkers_call(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public string ModifierName { get; } = "modifier_axe_berserkers_call_armor";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_axe_berserkers_call";

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }
    }
}