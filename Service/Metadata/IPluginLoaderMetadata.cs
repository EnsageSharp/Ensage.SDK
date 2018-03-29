// <copyright file="IPluginLoaderMetadata.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Metadata
{
    public interface IPluginLoaderMetadata
    {
        string Author { get; }

        string Description { get; }

        StartupMode Mode { get; }

        string Name { get; }

        int Delay { get; }

        HeroId[] Units { get; }

        string Version { get; }
    }
}