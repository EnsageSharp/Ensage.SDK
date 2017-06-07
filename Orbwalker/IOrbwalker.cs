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
        IServiceContext Context { get; }

        Vector3 OrbwalkingPoint { get; set; }

        OrbwalkerSettings Settings { get; }

        bool Attack(Unit target);

        bool CanAttack(Unit target);

        bool CanMove();

        float GetTurnTime(Entity unit);

        bool Move(Vector3 position);

        bool OrbwalkTo(Unit target);
    }
}