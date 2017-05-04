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
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportTargetSelectorManager]
    public class TargetSelectorManager : ITargetSelectorManager, IPartImportsSatisfiedNotification
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public TargetSelectorManager([Import] IServiceContext context)
        {
            this.Context = context;
        }

        public ITargetSelector Active { get; private set; }

        [ImportManyTargetSelector]
        public IEnumerable<Lazy<ITargetSelector, ITargetSelectorMetadata>> Selectors { get; protected set; }

        private TargetSelectorConfig Config { get; set; }

        private IServiceContext Context { get; }

        public void OnImportsSatisfied()
        {
            if (this.Config == null)
            {
                this.Config = new TargetSelectorConfig(this.Context, this.Selectors.Select(s => s.Metadata.Name).ToArray());
                this.Config.Active.Item.ValueChanged += this.OnValueChanged;
            }
            else
            {
                this.Config.UpdateModes(this.Selectors.Select(s => s.Metadata.Name).ToArray());
            }

            this.UpdateActive(this.Config.Active.Value.SelectedValue);
        }

        private void OnValueChanged(object sender, OnValueChangeEventArgs args)
        {
            this.UpdateActive(args.GetNewValue<StringList>().SelectedValue);
        }

        private void UpdateActive(string name)
        {
            Log.Debug($"Activate Mode {name}");

            this.Active?.Deactivate();
            this.Active = this.Selectors.FirstOrDefault(s => s.Metadata.Name == name)?.Value;
            this.Active?.Activate();
        }
    }
}