// <copyright file="IAuraAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public interface IAuraAbility
    {
        string AuraModifierName { get; }

        float AuraRadius { get; }
    }
}