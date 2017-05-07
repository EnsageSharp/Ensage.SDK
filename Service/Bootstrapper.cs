// <copyright file="Bootstrapper.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.AppDomain.Loader;
    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

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

            if (part.Metadata.Units != null)
            {
                foreach (var unit in part.Metadata.Units)
                {
                    this.Menu.Item<object>($"Unit: {unit}");
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

    public class Bootstrapper : AssemblyEntryPoint
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SDKConfig Config { get; private set; }

        public ContextContainer<IServiceContext> Default { get; private set; }

        public List<PluginContainer> PluginContainer { get; } = new List<PluginContainer>();

        public IEnumerable<Lazy<IPluginLoader, IPluginLoaderMetadata>> Plugins
        {
            get
            {
                return this.Default?.GetAll<IPluginLoader, IPluginLoaderMetadata>();
            }
        }

        public void BuildUp(object instance)
        {
            this.Default.BuildUp(instance);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return this.Default.GetAllInstances(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return this.Default.GetInstance(serviceType, key);
        }

        protected override void OnActivated()
        {
            UpdateManager.Subscribe(this.OnLoad);
        }

        protected override void OnDeactivated()
        {
            try
            {
                this.DeactivatePlugins();
                this.Default?.Dispose();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void ActivatePlugins()
        {
            foreach (var plugin in this.PluginContainer)
            {
                if (plugin.Mode == StartupMode.Manual && plugin.ActiveItem)
                {
                    plugin.Activate();
                }

                if (plugin.Mode == StartupMode.Auto && plugin.ActiveItem)
                {
                    plugin.Activate();
                }
            }
        }

        private void DeactivatePlugins()
        {
            foreach (var plugin in this.PluginContainer)
            {
                plugin.Deactivate();
            }
        }

        private void DiscoverPlugins()
        {
            foreach (var assembly in this.Plugins)
            {
                if (assembly.Metadata.Units == null)
                {
                    Log.Debug($"Found [{assembly.Metadata.Mode}] {assembly.Metadata.Name} | {assembly.Metadata.Author} | {assembly.Metadata.Version}");
                }
                else
                {
                    Log.Debug($"Found [{assembly.Metadata.Mode}] {assembly.Metadata.Name} | {assembly.Metadata.Author} | {assembly.Metadata.Version} | {string.Join(", ", assembly.Metadata.Units)}");
                }

                this.PluginContainer.Add(new PluginContainer(this.Config.Plugins.Factory, assembly));
            }
        }

        private void OnLoad()
        {
            if (ObjectManager.LocalHero == null)
            {
                return;
            }

            UpdateManager.Unsubscribe(this.OnLoad);

            try
            {
                var sw = Stopwatch.StartNew();

                Log.Debug("====================================================");
                Log.Debug($">> Ensage.SDK Bootstrap started");
                Log.Debug("====================================================");

                Log.Debug($">> Building Menu");
                this.Config = new SDKConfig();

                Log.Debug($">> Building Container for LocalHero");
                this.Default = ContainerFactory.CreateContainer(ObjectManager.LocalHero);
                this.Default.RegisterValue(this.Config);

                Log.Debug($">> Initializing Services");
                IoC.Initialize(this.BuildUp, this.GetInstance, this.GetAllInstances);

                Log.Debug($">> Searching for Plugins");
                this.DiscoverPlugins();

                Log.Debug($">> Activating Plugins");
                this.ActivatePlugins();

                sw.Stop();

                Log.Debug("====================================================");
                Log.Debug($">> Bootstrap completed in {sw.Elapsed}");
                Log.Debug("====================================================");
            }
            catch (ReflectionTypeLoadException e)
            {
                foreach (var exception in e.LoaderExceptions)
                {
                    Log.Error(exception);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}