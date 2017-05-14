// <copyright file="HitChance.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    /// <summary>
    ///     Represents the chance of hitting an enemy.
    /// </summary>
    public enum HitChance
    {
        /// <summary>
        ///     The target is immobile.
        /// </summary>
        Immobile = 8,

        /// <summary>
        ///     The unit is dashing.
        /// </summary>
        Dashing = 7,

        /// <summary>
        ///     Very high probability of hitting the target.
        /// </summary>
        VeryHigh = 6,

        /// <summary>
        ///     High probability of hitting the target.
        /// </summary>
        High = 5,

        /// <summary>
        ///     Medium probability of hitting the target.
        /// </summary>
        Medium = 4,

        /// <summary>
        ///     Low probability of hitting the target.
        /// </summary>
        Low = 3,

        /// <summary>
        ///     Impossible to hit the target.
        /// </summary>
        Impossible = 2,

        /// <summary>
        ///     The target is out of range.
        /// </summary>
        OutOfRange = 1,

        /// <summary>
        ///     The target is blocked by other units.
        /// </summary>
        Collision = 0
    }
}