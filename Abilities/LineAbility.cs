// <copyright file="LineAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class LineAbility : PointAbility
    {
        protected LineAbility(Ability ability)
            : base(ability)
        {
        }
    }
}