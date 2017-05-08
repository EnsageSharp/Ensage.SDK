// <copyright file="TargetSelectorManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Config;
    using Ensage.SDK.TargetSelector.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportTargetSelectorManager]
    public class TargetSelectorManager : ControllableService, ITargetSelectorManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private ITargetSelector active;

        [ImportingConstructor]
        public TargetSelectorManager([Import] IServiceContext context, [Import] Lazy<IParticleManager> particle)
        {
            this.Context = context;
            this.Particle = particle;
        }

        public ITargetSelector Active
        {
            get
            {
                return this.active;
            }

            set
            {
                if (value == null && this.active != null)
                {
                    Log.Debug($"Deactivate Selector {this.active}");
                    this.active.Deactivate();
                    this.active = null;
                    return;
                }

                if (EqualityComparer<ITargetSelector>.Default.Equals(this.active, value))
                {
                    return;
                }

                Log.Debug($"Activate Selector {value}");
                this.active?.Deactivate();
                this.active = value;
                this.active?.Activate();
            }
        }

        public TargetSelectorConfig Config { get; private set; }

        public Lazy<IParticleManager> Particle { get; }

        [ImportMany(typeof(ITargetSelector))]
        public IEnumerable<Lazy<ITargetSelector, ITargetSelectorMetadata>> Selectors { get; private set; }

        private IServiceContext Context { get; }

        protected override void OnActivate()
        {
            this.Config = new TargetSelectorConfig(this.Selectors.Select(e => e.Metadata.Name));
            this.Config.Selection.Item.ValueChanged += this.OnSelectionChanged;

            // activate selection
            this.UpdateActive(this.Config.Selection);

            UpdateManager.Subscribe(this.OnDrawingsUpdate, 250);
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.OnDrawingsUpdate);
            this.Particle.Value.Remove("ActiveTargetSelectorTarget");

            this.Active?.Deactivate();
            this.Active = null;

            this.Config.Selection.Item.ValueChanged -= this.OnSelectionChanged;
            this.Config.Dispose();
        }

        private void OnDrawingsUpdate()
        {
            if (this.Active == null)
            {
                return;
            }

            var target = this.Active.GetTargets()?.FirstOrDefault();

            if (target != null)
            {
                this.Particle.Value.DrawRange(target, "ActiveTargetSelectorTarget", target.HullRadius * 4, Color.Yellow);
            }
            else
            {
                this.Particle.Value.Remove("ActiveTargetSelectorTarget");
            }
        }

        private void OnSelectionChanged(object sender, OnValueChangeEventArgs args)
        {
            this.UpdateActive(args.GetNewValue<StringList>().SelectedValue);
        }

        private void UpdateActive(string name)
        {
            this.Active?.Deactivate();

            if (name == "None")
            {
                this.Active = null;
                return;
            }

            if (name.StartsWith("Auto"))
            {
                name = "Near Mouse";
            }

            this.Active = this.Selectors.FirstOrDefault(s => s.Metadata.Name == name)?.Value;
        }
    }
}