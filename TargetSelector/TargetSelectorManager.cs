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
    using Ensage.SDK.TargetSelector.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportTargetSelectorManager]
    public class TargetSelectorManager : ITargetSelectorManager, IPartImportsSatisfiedNotification
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public TargetSelectorManager([Import] IServiceContext context, [Import] IParticleManager particle)
        {
            this.Context = context;
            this.Particle = particle;
            this.Config = new TargetSelectorConfig();
            this.Context.Container.BuildUp(this.Config);
            this.Context.Container.RegisterValue(this.Config);

            this.Config.Active.Item.ValueChanged += this.OnValueChanged;

            UpdateManager.Subscribe(this.OnDrawingsUpdate, 250);
        }

        public ITargetSelector Active { get; private set; }

        public IParticleManager Particle { get; }

        [ImportManyTargetSelector]
        public IEnumerable<Lazy<ITargetSelector, ITargetSelectorMetadata>> Selectors { get; protected set; }

        private TargetSelectorConfig Config { get; }

        private IServiceContext Context { get; }

        public void OnImportsSatisfied()
        {
            this.UpdateActive(this.Config.Active.Value.SelectedValue);
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
                this.Particle.DrawRange(target, "ActiveTargetSelectorTarget", target.HullRadius * 4, Color.Yellow);
            }
            else
            {
                this.Particle.Remove("ActiveTargetSelectorTarget");
            }
        }

        private void OnValueChanged(object sender, OnValueChangeEventArgs args)
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

            Log.Debug($"Activate Mode {name}");
            this.Active = this.Selectors.FirstOrDefault(s => s.Metadata.Name == name)?.Value;
            this.Active?.Activate();
        }
    }
}