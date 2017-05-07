// <copyright file="TargetSelectorPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.TargetSelector;

    [ExportPlugin("Target Selector", StartupMode.Auto)]
    public class TargetSelectorPlugin : Plugin
    {
        [ImportingConstructor]
        public TargetSelectorPlugin([Import] ITargetSelectorManager service)
        {
            this.Service = service;
        }

        public ITargetSelectorManager Service { get; }
    }
}