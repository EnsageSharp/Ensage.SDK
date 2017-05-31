// <copyright file="IOrbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using Ensage.SDK.Service;

    using PlaySharp.Toolkit.Helper;

    using SharpDX;

    public interface IOrbwalker : IControllable
    {
        OrbwalkerConfig Config { get; }

        IServiceContext Context { get; }

        float TurnEndTime { get; }

        bool Attack(Unit target);

        bool CanAttack(Unit target);

        bool CanMove();

        bool Move(Vector3 position);
    }
}