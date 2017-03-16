// <copyright file="ConeAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class ConeAbility : PointAbility
    {
        protected ConeAbility(Ability ability)
            : base(ability)
        {
        }
    }
}