// <copyright file="IActiveAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using SharpDX;

    public interface IActiveAbility
    {
        float CastPoint { get; }

        bool IsChanneling { get; }

        void UseAbility(bool queued = false);

        void UseAbility(Vector3 target, bool queued = false);

        void UseAbility(Unit target, bool queued = false);
    }
}