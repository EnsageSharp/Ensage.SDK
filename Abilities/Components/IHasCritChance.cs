// <copyright file="IHasCritChance.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public interface IHasCritChance : IHasProcChance
    {
        /// <summary>
        ///     Gets the damage multiplier for the critical attack.
        /// </summary>
        float CritMultiplier { get; }
    }
}