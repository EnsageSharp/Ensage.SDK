// <copyright file="IHasProcChance.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public interface IHasProcChance
    {
        /// <summary>
        ///     Gets the proc chance with from 0 to 1.
        /// </summary>
        float ProcChance { get; }
    }
}