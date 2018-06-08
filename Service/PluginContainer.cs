// <copyright file="PluginContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Service.Metadata;

    

    using PlaySharp.Toolkit.Helper;
    using NLog;

    public class PluginContainer : IActivatable, IDeactivatable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public PluginContainer(MenuFactory factory, Lazy<IPluginLoader, IPluginLoaderMetadata> part)
        {
            this.Part = part;
            this.Assembly = part.Value.GetType().Assembly;
            this.Menu = factory.Item(this.Part.Metadata.Name, this.Mode == StartupMode.Auto);
            this.Menu.Item.SetTooltip($"{part.Metadata.Author} - {part.Metadata.Version} - {this.Assembly.GetName().Name}");
            this.Menu.Item.ValueChanged += this.OnActiveValueChanged;

            Log.Info($"Created {this}");
        }

        public Assembly Assembly { get; }

        public bool IsActive => this.Part.Value.IsActive;

        public MenuItem<bool> Menu { get; }

        public IPluginLoaderMetadata Metadata => this.Part.Metadata;

        public StartupMode Mode => this.Part.Metadata.Mode;

        private Lazy<IPluginLoader, IPluginLoaderMetadata> Part { get; }

        public void Activate()
        {
            try
            {
                if (this.IsActive)
                {
                    return;
                }

                Log.Info($"Activating {this.Metadata.Name}");
                this.Part.Value.Activate();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
        }

        public void Deactivate()
        {
            try
            {
                if (this.Part.IsValueCreated && this.Part.Value.IsActive)
                {
                    Log.Info($"Deactivate {this.Metadata.Name}");
                    this.Part.Value.Deactivate();
                }
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
        }

        public override string ToString()
        {
            if (this.Metadata.Units != null)
            {
                return $"Plugin {this.Metadata.Name} | {this.Mode} | {this.Metadata.Author} | {this.Metadata.Version} | {string.Join(", ", this.Metadata.Units)}";
            }

            return $"Plugin {this.Metadata.Name} | {this.Mode} | {this.Metadata.Author} | {this.Metadata.Version}";
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