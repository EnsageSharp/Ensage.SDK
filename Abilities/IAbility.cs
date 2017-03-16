// <copyright file="IAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public interface IAbility
    {
        Ability Ability { get; }

        float GetDamage(params Unit[] target);
    }
}