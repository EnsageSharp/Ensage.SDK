// <copyright file="PointAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class PointAbility : ActiveAbility
    {
        protected PointAbility(Ability ability)
            : base(ability)
        {
        }
    }
}