// <copyright file="IEnsageServiceContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    public interface IEnsageServiceContext : IServiceContext
    {
        Unit Owner { get; }
    }
}