// <copyright file="BaseAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class BaseAbility : IAbility
    {
        protected BaseAbility(Ability ability)
        {
            this.Ability = ability;
        }

        public Ability Ability { get; }

        public abstract float GetDamage(params Unit[] target);
    }
}