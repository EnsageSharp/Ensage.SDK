// <copyright file="IOrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    public interface IOrbwalkingMode
    {
        bool CanExecute { get; }

        void Execute();
    }
}