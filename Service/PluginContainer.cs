// <copyright file="PluginContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public class PluginContainer : IActivatable, IDeactivatable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PluginContainer(MenuFactory factory, Lazy<IPluginLoader, IPluginLoaderMetadata> part)
        {
            this.Part = part;
            this.Assembly = part.Value.GetType().Assembly;

            this.Menu = factory.Menu(this.Part.Metadata.Name);

            this.ActiveItem = this.Menu.Item("Activate", part.Metadata.Mode == StartupMode.Auto);
            this.ActiveItem.Item.ValueChanged += this.OnActiveValueChanged;

            this.Menu.Item<object>($"Author: {part.Metadata.Author}");
            this.Menu.Item<object>($"Version: {part.Metadata.Version}");
            this.Menu.Item<object>($"Assembly: {this.Assembly.GetName().Name}");
            this.Menu.Item<object>($"Mode: {part.Metadata.Mode}");

            if (this.Metadata.Units != null)
            {
                foreach (var unit in this.Metadata.Units)
                {
                    var item = this.Menu.Item<object>($"Unit: {unit}");

                    if (unit == ObjectManager.LocalHero.HeroId)
                    {
                        item.Item.SetFontColor(Color.Green);
                    }
                }

                if (!this.Metadata.Units.Contains(ObjectManager.LocalHero.HeroId))
                {
                    this.ActiveItem.Item.DisplayName = $"Activate ({ObjectManager.LocalHero.HeroId} not supported)";
                    this.ActiveItem.Item.SetFontColor(Color.Red);
                }
            }
        }

        public MenuItem<bool> ActiveItem { get; }

        public Assembly Assembly { get; }

        public bool IsActive => this.Part.Value.IsActive;

        public MenuFactory Menu { get; }

        public IPluginLoaderMetadata Metadata => this.Part.Metadata;

        public StartupMode Mode => this.Part.Metadata.Mode;

        private Lazy<IPluginLoader, IPluginLoaderMetadata> Part { get; }

        public void Activate()
        {
            try
            {
                if (this.Part.Value.IsActive)
                {
                    return;
                }

                if (this.Mode == StartupMode.Auto)
                {
                    if (this.Metadata.Units != null && this.Metadata.Units.Length > 0)
                    {
                        if (!this.Metadata.Units.Contains(ObjectManager.LocalHero.HeroId))
                        {
                            return;
                        }
                    }
                }

                Log.Info($"Activate {this.Metadata.Name}");
                this.Part.Value.Activate();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void Deactivate()
        {
            if (this.Part.IsValueCreated && this.Part.Value.IsActive)
            {
                Log.Info($"Deactivate {this.Metadata.Name}");
                this.Part.Value.Deactivate();
            }
        }

        private void OnActiveValueChanged(object sender, OnValueChangeEventArgs args)
        {
            if (args.GetNewValue<bool>())
            {
                this.Activate();
            }
            else
            {
                this.Deactivate();
            }
        }
    }
}