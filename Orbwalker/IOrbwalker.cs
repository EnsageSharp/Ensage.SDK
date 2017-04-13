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

        bool IsOrbwalking { get; set; }

        Key Key { get; set; }

        void Orbwalk(Vector3 position);

        void Orbwalk(Unit target);
    }
}