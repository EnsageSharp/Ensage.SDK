// <copyright file="IHasDamageAmplifier.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Components
{
    public interface IHasDamageAmplifier
    {
        DamageType AmplifierType { get; }

        float DamageAmplification { get; }
    }
}