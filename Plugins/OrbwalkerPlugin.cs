// <copyright file="OrbwalkerPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin("Orbwalker", StartupMode.Auto)]
    public class OrbwalkerPlugin : Plugin
    {
        [ImportingConstructor]
        public OrbwalkerPlugin([Import] IOrbwalker service)
        {
            this.Service = service;
        }

        public IOrbwalker Service { get; }
    }
}