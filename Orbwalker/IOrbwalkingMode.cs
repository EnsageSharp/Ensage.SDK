// <copyright file="IOrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System.Threading;
    using System.Threading.Tasks;

    using PlaySharp.Toolkit.Helper;

    public interface IOrbwalkingMode : IControllable, IExecutable
    {
    }

    public interface IOrbwalkingModeAsync : IControllable, IExecutableAsync
    {
    }

    public interface IExecutableAsync
    {
        bool CanExecute { get; }

        Task ExecuteAsync(CancellationToken token = default(CancellationToken));
    }

    public interface IExecutable
    {
        bool CanExecute { get; }

        void Execute();
    }
}