// <copyright file="AreaOfEffectAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;

    using Ensage.SDK.Extensions;

    public abstract class AreaOfEffectAbility : RangedAbility, IAreaOfEffectAbility
    {
        protected AreaOfEffectAbility(Ability ability)
            : base(ability)
        {
        }

        public virtual float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < (this.CastRange + this.Radius));
        }
    }
}