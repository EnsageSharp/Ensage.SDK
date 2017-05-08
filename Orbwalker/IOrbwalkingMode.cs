// <copyright file="IOrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using PlaySharp.Toolkit.Helper;

    public interface IOrbwalkingMode : IControllable
    {
        bool CanExecute { get; }

        void Execute();
    }
}