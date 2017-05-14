// <copyright file="IOrbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Service;

    using PlaySharp.Toolkit.Helper;

    using SharpDX;

    public interface IOrbwalker : IControllable
    {
        OrbwalkerConfig Config { get; }

        IServiceContext Context { get; }

        bool Attack(Unit target);

        bool CanAttack(Unit target);

        bool CanMove();

        float GetTurnTime(Entity unit);

        bool Move(Vector3 position);

        bool OrbwalkTo(Unit target);

        void RegisterMode(IOrbwalkingMode mode);

        void UnregisterMode(IOrbwalkingMode mode);
    }
}