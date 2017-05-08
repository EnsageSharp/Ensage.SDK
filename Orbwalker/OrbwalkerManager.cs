// <copyright file="OrbwalkerManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportOrbwalkerManager]
    public class OrbwalkerManager : ControllableService, IOrbwalkerManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IOrbwalker active;

        [ImportingConstructor]
        public OrbwalkerManager([Import] IServiceContext context)
        {
            this.Context = context;
        }

        public IOrbwalker Active
        {
            get
            {
                return this.active;
            }

            set
            {
                if (value == null && this.active != null)
                {
                    Log.Debug($"Deactivate Orbwalker {this.active}");
                    this.active.Deactivate();
                    this.active = null;
                    return;
                }

                if (EqualityComparer<IOrbwalker>.Default.Equals(this.active, value))
                {
                    return;
                }

                Log.Debug($"Activate Orbwalker {value}");
                this.active?.Deactivate();
                this.active = value;
                this.active?.Activate();
            }
        }

        public OrbwalkerConfig Config { get; private set; }

        [ImportMany(typeof(IOrbwalker))]
        public IEnumerable<Lazy<IOrbwalker, IOrbwalkerMetadata>> Orbwalkers { get; private set; }

        private IServiceContext Context { get; }

        protected override void OnActivate()
        {
            try
            {
                this.Config = new OrbwalkerConfig(this.Context.Owner.GetDisplayName(), this.Orbwalkers.Select(e => e.Metadata.Name).ToArray());
                this.Config.Selection.Item.ValueChanged += this.OnSelectionChanged;

                // activate selection
                this.Active = this.Orbwalkers.FirstOrDefault(e => e.Metadata.Name == this.Config.Selection)?.Value;

                Log.Warn($"selection: {this.Active}");
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
        }

        protected override void OnDeactivate()
        {
            this.Active?.Deactivate();
            this.Active = null;

            this.Config.Selection.Item.ValueChanged -= this.OnSelectionChanged;
            this.Config?.Dispose();
        }

        private void OnSelectionChanged(object sender, OnValueChangeEventArgs args)
        {
            // update selection
            var name = args.GetNewValue<StringList>().SelectedValue;
            this.Active = this.Orbwalkers.FirstOrDefault(e => e.Metadata.Name == name)?.Value;
        }
    }
}