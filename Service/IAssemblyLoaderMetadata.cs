// <copyright file="IAssemblyLoaderMetadata.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    public interface IAssemblyLoaderMetadata
    {
        string Author { get; }

        string Name { get; }

        HeroId[] Units { get; }

        string Version { get; }
    }
}