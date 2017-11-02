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

        public string ModifierName { get; } = "modifier_axe_berserkers_call_armor";

        public float Radius
        {
            get
            {
                var radius = this.Ability.GetAbilitySpecialData("radius");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_axe_2);
                if (talent?.Level > 0)
                {
                    radius += talent.GetAbilitySpecialData("value");
                }

                return radius;
            }
        }

        public string TargetModifierName { get; } = "modifier_axe_berserkers_call";

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }
    }
}