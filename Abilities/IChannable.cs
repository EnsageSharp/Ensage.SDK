// <copyright file="IChannable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public interface IChannable
    {
        /// <summary>
        ///     Gets the maximum duration of the channeling ability.
        /// </summary>
        float Duration { get; }

        /// <summary>
        ///     Gets a value indicating whether the ability is currently channeled.
        /// </summary>
        bool IsChanneling { get; }

        /// <summary>
        ///     Gets the remaining duration of the ability, while it's being channeled.
        /// </summary>
        float RemainingDuration { get; }
    }
}