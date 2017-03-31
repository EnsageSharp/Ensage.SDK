// <copyright file="IOrbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Windows.Input;

    using SharpDX;

    public interface IOrbwalker : IDisposable
    {
        event EventHandler<EventArgs> Attacked;

        event EventHandler<EventArgs> Attacking;

        Key Key { get; set; }

        bool IsOrbwalking { get; set; }

        void Orbwalk(Vector3 position);

        void Orbwalk(Unit target);
    }
}