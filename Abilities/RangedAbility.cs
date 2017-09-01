// <copyright file="RangedAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;

    public abstract class RangedAbility : ActiveAbility
    {
        protected RangedAbility(Ability ability)
            : base(ability)
        {
        }

        public override float CastRange
        {
            get
            {
                return this.Ability.GetCastRange() + this.Owner.BonusCastRange();
            }
        }
    }
}