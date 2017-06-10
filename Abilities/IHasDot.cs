// <copyright file="IHasDot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public interface IHasDot : IHasTargetModifier
    {
        /// <summary>
        /// Gets the duration of the dot. 
        /// </summary>
        float Duration { get; }

        /// <summary>
        /// Returns true when the dot has an initial damage instance. Use <see cref="ActiveAbility.GetDamage"/> to get the damage value.
        /// </summary>
        bool HasInitialDamage { get; }

        /// <summary>
        /// Gets the time between damage instances of the dot.
        /// </summary>
        float TickRate { get; }

        /// <summary>
        /// Gets the dot's damage of each tick.
        /// </summary>
        /// <param name="target">The target which has the dot.</param>
        /// <returns>Damage of each tick.</returns>
        float GetTickDamage(params Unit[] target);

        /// <summary>
        /// Gets the total damage of the dot, including the initial damage and the total possible tick damage.
        /// </summary>
        /// <param name="target">The target(s) which have the dot applied.</param>
        /// <returns>Total damage.</returns>
        float GetTotalDamage(params Unit[] target);
    }
}