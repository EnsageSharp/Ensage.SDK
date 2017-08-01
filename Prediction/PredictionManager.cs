// <copyright file="PredictionManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Prediction.Metadata;
    using Ensage.SDK.Service;

    [ExportPredictionManager]
    public class PredictionManager : ServiceManager<IPrediction, IPredictionMetadata>, IPredictionManager, IPartImportsSatisfiedNotification
    {
        [ImportingConstructor]
        public PredictionManager([Import] IServiceContext context)
        {
            this.Context = context;
        }

        public SDKConfig.PredictionConfig Config { get; private set; }

        public IServiceContext Context { get; }

        [ImportMany(typeof(IPrediction), AllowRecomposition = true)]
        public override IEnumerable<Lazy<IPrediction, IPredictionMetadata>> Services { get; protected set; }

        public PredictionOutput GetPrediction(PredictionInput input)
        {
            return this.Active?.GetPrediction(input);
        }

        public void OnImportsSatisfied()
        {
            this.Context.Config.Settings.UpdatePredictions(this.Services.Select(e => e.Metadata.Name));
        }

        protected override IPrediction GetSelection()
        {
            return this.Services.First(s => s.Metadata.Name == this.Context.Config.Settings.PredictionSelection).Value;
        }

        protected override void OnActivate()
        {
            this.Config = new SDKConfig.PredictionConfig(this.Context.Config.Factory);
            this.Context.Config.Settings.PredictionSelection.PropertyChanged += this.OnSelectionChanged;
            this.OnImportsSatisfied();

            this.Active = this.GetSelection();
        }

        protected override void OnDeactivate()
        {
            this.Active = null;

            this.Context.Config.Settings.PredictionSelection.PropertyChanged -= this.OnSelectionChanged;
            this.Config.Dispose();
        }

        private void OnSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Active = this.GetSelection();
        }
    }
}