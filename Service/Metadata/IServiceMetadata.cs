// <copyright file="IServiceMetadata.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Metadata
{
    using System.ComponentModel;
    using System.Security;

    [SecuritySafeCritical]
    public interface IServiceMetadata
    {
        [DefaultValue("")]
        string Description { get; }

        [DefaultValue("Default")]
        string Name { get; }

        [DefaultValue("")]
        string Version { get; }
    }
}