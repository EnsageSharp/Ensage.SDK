// <copyright file="TargetSelectorManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    

    using PlaySharp.Toolkit.Helper.Annotations;
    using NLog;

    using SharpDX;

    [ExportTargetSelectorManager]
    public sealed class TargetSelectorManager : ServiceManager<ITargetSelector, ITargetSelectorMetadata>, ITargetSelectorManager, IPartImportsSatisfiedNotification
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private IUpdateHandler updateHandler;

        [ImportingConstructor]
        public TargetSelectorManager([Import] IServiceContext context, [Import] Lazy<IParticleManager> particle)
        {
            this.Context = context;
            this.Particle = particle;
        }

        public SDKConfig.TargetSelectorConfig Config { get; private set; }

        public IServiceContext Context { get; }

        public Lazy<IParticleManager> Particle { get; }

        [ImportMany(typeof(ITargetSelector), AllowRecomposition = true)]
        public override IEnumerable<Lazy<ITargetSelector, ITargetSelectorMetadata>> Services { get; protected set; }

        public IEnumerable<Unit> GetTargets()
        {
            return this.Active?.GetTargets() ?? new Unit[0];
        }

        public void OnImportsSatisfied()
        {
            this.Context.Config.Settings.UpdateTargetSelectors(this.Services.Select(e => e.Metadata.Name).OrderBy(e => e != "Near Mouse"));
        }

        [CanBeNull]
        protected override ITargetSelector GetSelection()
        {
            return this.Services.First(s => s.Metadata.Name == this.Context.Config.Settings.TargetSelectorSelection).Value;
        }

        protected override void OnActivate()
        {
            this.Config = new SDKConfig.TargetSelectorConfig(this.Context.Config.Factory);
            this.Context.Config.Settings.TargetSelectorSelection.PropertyChanged += this.OnSelectionChanged;
            this.OnImportsSatisfied();

            // activate selection
            this.Active = this.GetSelection();

            this.updateHandler = UpdateManager.Subscribe(this.OnDrawingsUpdate, 250, this.Config.ShowTargetParticle);
            this.Config.ShowTargetParticle.PropertyChanged += this.ShowTargetParticleOnPropertyChanged;
        }

        protected override void OnDeactivate()
        {
            this.Config.ShowTargetParticle.PropertyChanged -= this.ShowTargetParticleOnPropertyChanged;
            UpdateManager.Unsubscribe(this.OnDrawingsUpdate);
            this.Particle.Value.Remove("ActiveTargetSelectorTarget");

            this.Active = null;

            this.Context.Config.Settings.TargetSelectorSelection.PropertyChanged -= this.OnSelectionChanged;
            this.Config.Dispose();
        }

        private void OnDrawingsUpdate()
        {
            var target = this.GetTargets().FirstOrDefault();

            if (target != null)
            {
                this.Particle.Value.DrawRange(target, "ActiveTargetSelectorTarget", target.HullRadius * 4, Color.Yellow);
            }
            else
            {
                this.Particle.Value.Remove("ActiveTargetSelectorTarget");
            }
        }

        private void OnSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Active = this.GetSelection();
        }

        private void ShowTargetParticleOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            this.updateHandler.IsEnabled = this.Config.ShowTargetParticle;
            this.Particle.Value.Remove("ActiveTargetSelectorTarget");
        }
    }
}