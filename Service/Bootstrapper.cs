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

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.AppDomain.Loader;
    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    public class Bootstrapper : AssemblyEntryPoint
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SDKConfig Config { get; private set; }

        public ContextContainer<IServiceContext> Default { get; private set; }

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
            var id = ObjectManager.LocalHero.HeroId;

            foreach (var plugin in this.Plugins)
            {
                try
                {
                    if (plugin.IsValueCreated && plugin.Value.IsActive)
                    {
                        continue;
                    }

                    if (plugin.Metadata.Mode == StartupMode.Auto)
                    {
                        if (plugin.Metadata.Units != null && plugin.Metadata.Units.Length > 0)
                        {
                            if (!plugin.Metadata.Units.Contains(id))
                            {
                                continue;
                            }
                        }

                        Log.Info($"Activate {plugin.Metadata.Name}");
                        plugin.Value.Activate();
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private void DeactivatePlugins()
        {
            foreach (var assembly in this.Plugins)
            {
                try
                {
                    if (assembly.IsValueCreated)
                    {
                        Log.Info($"Deactivate {assembly.Metadata.Name}");
                        assembly.Value.Deactivate();
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
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

                Log.Debug($">> Searching for IAssemblyLoader Plugins");
                this.PrintPlugins();

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

        private void PrintPlugins()
        {
            foreach (var assembly in this.Plugins)
            {
                if (assembly.Metadata.Units == null)
                {
                    Log.Debug($"Found [{assembly.Metadata.Mode}] {assembly.Metadata.Name} | {assembly.Metadata.Author} | {assembly.Metadata.Version}");
                }
                else
                {
                    Log.Debug(
                        $"Found [{assembly.Metadata.Mode}] {assembly.Metadata.Name} | {assembly.Metadata.Author} | {assembly.Metadata.Version} | {string.Join(", ", assembly.Metadata.Units)}");
                }
            }
        }
    }
}