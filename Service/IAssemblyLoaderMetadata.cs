// <copyright file="IAssemblyLoaderMetadata.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System.ComponentModel;

    public interface IAssemblyLoaderMetadata
    {
        [DefaultValue("Ensage")]
        string Author { get; }

        string Name { get; }

        [DefaultValue(null)]
        HeroId[] Units { get; }

        [DefaultValue("1.0.0.0")]
        string Version { get; }
    }
}