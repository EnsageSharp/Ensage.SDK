// <copyright file="Bootstrapper.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Logger;
    using Ensage.SDK.Service.Metadata;

    

    using PlaySharp.Toolkit.AppDomain.Loader;
    using PlaySharp.Toolkit.Helper;
    using NLog;

    public class Bootstrapper : AssemblyEntryPoint
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Bootstrapper()
        {
            Logging.Init();
        }

        public ContextContainer<IServiceContext> Default { get; private set; }

        public List<PluginContainer> PluginContainer { get; } = new List<PluginContainer>();

        public IEnumerable<Lazy<IPluginLoader, IPluginLoaderMetadata>> Plugins
        {
            get
            {
                return this.Default.GetExports<IPluginLoader, IPluginLoaderMetadata>();
            }
        }

        private EnsageServiceContext Context { get; set; }

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
                this.Context.MenuManager.Dispose();
                this.Context.Renderer.Dispose();
                this.Context.TextureManager.Dispose();
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
        }

        private void ActivatePlugins()
        {
            foreach (var plugin in this.PluginContainer.OrderBy(e => e.Metadata.Priority))
            {
                if (plugin.Menu && !plugin.IsActive)
                {
                    UpdateManager.BeginInvoke(plugin.Activate, plugin.Metadata.Priority);
                }
            }
        }

        private void DeactivatePlugins()
        {
            foreach (var plugin in this.PluginContainer.OrderByDescending(e => e.Metadata.Priority))
            {
                try
                {
                    plugin.Deactivate();
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }

        private void DiscoverPlugins()
        {
            foreach (var assembly in this.Plugins.OrderBy(e => e.Metadata.Priority))
            {
                try
                {
                    Log.Debug($"Found {assembly.Metadata.Name}");

                    if (assembly.Metadata.IsSupported())
                    {
                        this.PluginContainer.Add(new PluginContainer(this.Context.Config.Plugins.Factory, assembly));
                    }
                    else
                    {
                        Log.Warn($"Plugin not supported {assembly.Metadata.Name}");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private void OnBootstrap()
        {
            try
            {
                if (ObjectManager.LocalHero == null)
                {
                    throw new Exception("Local Hero is null");
                }

                var sw = Stopwatch.StartNew();

                Log.Debug("====================================================");
                Log.Debug($">> Ensage.SDK Bootstrap started");
                Log.Debug("====================================================");

                Game.UnhandledException += this.OnUnhandledException;

                Log.Debug($">> Building Context for LocalHero");
                this.Context = new EnsageServiceContext(ObjectManager.LocalHero);

                this.Default = this.Context.Container;
                this.Default.RegisterValue(this);

                Log.Debug($">> Initializing Services");
                IoC.Initialize(this.BuildUp, this.GetInstance, this.GetAllInstances);

                Log.Debug($">> Searching for Plugins");
                this.DiscoverPlugins();

                UpdateManager.BeginInvoke(this.ActivatePlugins, 250);

                sw.Stop();

                Log.Debug("====================================================");
                Log.Debug($">> Bootstrap completed in {sw.Elapsed}");
                Log.Debug("====================================================");
            }
            catch (ReflectionTypeLoadException e)
            {
                foreach (var exception in e.LoaderExceptions)
                {
                    Log.Fatal(exception);
                }
            }
            catch (Exception e)
            {
                Log.Fatal(e);
            }
        }

        private void OnLoad()
        {
            if (ObjectManager.LocalHero == null)
            {
                return;
            }

            UpdateManager.Unsubscribe(this.OnLoad);
            UpdateManager.BeginInvoke(this.OnBootstrap);
        }

        private void OnUnhandledException(object sender, Exception e)
        {
            Log.Error(e);
        }
    }
}