// <copyright file="item_mjollnir.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_mjollnir : RangedAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public item_mjollnir(Item item)
            : base(item)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("static_radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_item_mjollnir_static";

        public override bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }
    }
}