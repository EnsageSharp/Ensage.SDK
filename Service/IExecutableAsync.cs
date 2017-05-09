// <copyright file="IExecutableAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IExecutableAsync
    {
        bool CanExecute { get; }

        Task ExecuteAsync(CancellationToken token = default(CancellationToken));
    }
}