// <copyright file="PredictionPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Prediction;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin("Prediction", StartupMode.Auto, priority: 0, description: "SDK Prediction Framework")]
    public class PredictionPlugin : Plugin
    {
        [ImportingConstructor]
        public PredictionPlugin([Import] Lazy<IPredictionManager> service)
        {
            this.Service = service;
        }

        public Lazy<IPredictionManager> Service { get; }

        protected override void OnActivate()
        {
            this.Service.Value.Activate();
        }

        protected override void OnDeactivate()
        {
            this.Service.Value.Deactivate();
        }
    }
}