// <copyright file="IExecutable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    public interface IExecutable
    {
        bool CanExecute { get; }

        void Execute();
    }
}