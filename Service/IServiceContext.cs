// <copyright file="IServiceContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    public interface IServiceContext
    {
        ContextContainer<IServiceContext> Container { get; }

        Hero Owner { get; }
    }
}