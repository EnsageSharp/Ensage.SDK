// <copyright file="SDKConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class SDKConfig
    {
        public SDKConfig()
        {
            this.Factory = MenuFactory.Create("Ensage.SDK");
            this.Factory.Target.Style = FontStyle.Bold;

            this.Plugins = new PluginsConfig(this.Factory);
            this.Settings = new SettingsConfig(this.Factory);

            this.Orbwalker = new OrbwalkerConfig(this.Factory);
            this.Prediction = new PredictionConfig(this.Factory);
            this.TargetSelector = new TargetSelectorConfig(this.Factory);
        }

        public MenuFactory Factory { get; }

        public OrbwalkerConfig Orbwalker { get; }

        public PluginsConfig Plugins { get; }

        public PredictionConfig Prediction { get; }

        public SettingsConfig Settings { get; }

        public TargetSelectorConfig TargetSelector { get; }

        public class OrbwalkerConfig : IDisposable
        {
            private bool disposed;

            public OrbwalkerConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Orbwalker");
                this.TickRate = this.Factory.Item("Tickrate", new Slider(100, 0, 200));
            }

            public MenuFactory Factory { get; }

            public MenuItem<Slider> TickRate { get; }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (this.disposed)
                {
                    return;
                }

                if (disposing)
                {
                    this.Factory.Dispose();
                }

                this.disposed = true;
            }
        }

        public class PluginsConfig
        {
            public PluginsConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Plugins");
            }

            public MenuFactory Factory { get; }
        }

        public class PredictionConfig : IDisposable
        {
            private bool disposed;

            public PredictionConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Prediction");
            }

            public MenuFactory Factory { get; }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (this.disposed)
                {
                    return;
                }

                if (disposing)
                {
                    this.Factory.Dispose();
                }

                this.disposed = true;
            }
        }

        public class SettingsConfig
        {
            public SettingsConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Settings");

                this.OrbwalkerSelection = this.Factory.Item<StringList>("Orbwalker");
                this.TargetSelectorSelection = this.Factory.Item<StringList>("Target Selector");
                this.PredictionSelection = this.Factory.Item<StringList>("Prediction");

                this.ErrorReporting = this.Factory.Item("Error Reporting", true);
            }

            public MenuItem<bool> ErrorReporting { get; }

            public MenuFactory Factory { get; }

            public MenuItem<StringList> OrbwalkerSelection { get; }

            public MenuItem<StringList> PredictionSelection { get; }

            public MenuItem<StringList> TargetSelectorSelection { get; }

            internal void UpdateOrbwalkers(IEnumerable<string> names)
            {
                this.OrbwalkerSelection.Value = new StringList(names.ToArray());
            }

            internal void UpdatePredictions(IEnumerable<string> names)
            {
                this.PredictionSelection.Value = new StringList(names.ToArray());
            }

            internal void UpdateTargetSelectors(IEnumerable<string> names)
            {
                this.TargetSelectorSelection.Value = new StringList(names.ToArray());
            }
        }

        public class TargetSelectorConfig : IDisposable
        {
            private bool disposed;

            public TargetSelectorConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Target Selector");
            }

            public MenuFactory Factory { get; }

            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (this.disposed)
                {
                    return;
                }

                if (disposing)
                {
                    this.Factory.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}