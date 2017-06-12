// <copyright file="AreaOfEffectAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
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
                return this.Ability.GetAbilitySpecialData(this.RadiusName);
            }
        }

        protected virtual string RadiusName { get; } = "radius";
    }
}