// <copyright file="IDotAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public interface IDotAbility
    {
        float Duration { get; }

        string ModifierName { get; }

        float TickRate { get; }

        float GetTickDamage(params Unit[] target);

        float GetTotalDamage(params Unit[] target);
    }
}