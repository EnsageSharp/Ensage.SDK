// <copyright file="AbilityEventArgs.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    public class AbilityEventArgs : EventArgs
    {
        public AbilityEventArgs(DetectedAbility ability)
        {
            this.Ability = ability;
            this.Caster = ability.Ability.Owner as Unit;
        }

        public DetectedAbility Ability { get; }

        public Unit Caster { get; }
    }
}