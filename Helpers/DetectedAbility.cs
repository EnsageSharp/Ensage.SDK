// <copyright file="DetectedAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class DetectedAbility
    {
        public DetectedAbility([NotNull] Ability ability)
        {
            if (ability == null || !ability.IsValid)
            {
                throw new ArgumentNullException(nameof(ability));
            }

            this.Ability = ability;
            this.DetectionTime = Game.GameTime;
        }

        public Ability Ability { get; }

        public float DetectionTime { get; }
    }
}