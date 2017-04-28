// <copyright file="Bootstrapper.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
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

        public IEnumerable<Lazy<IAssemblyLoader, IAssemblyLoaderMetadata>> Assemblies
        {
            get
            {
                return this.Default?.GetAll<IAssemblyLoader, IAssemblyLoaderMetadata>();
            }
        }

        public SDKConfig Config { get; private set; }

        public ContextContainer<IServiceContext> Default { get; private set; }

        private bool ExportsChanged { get; set; }

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

            foreach (var assembly in this.Assemblies)
            {
                try
                {
                    if (assembly.IsValueCreated)
                    {
                        continue;
                    }

                    if (assembly.Metadata.Units != null && assembly.Metadata.Units.Length > 0)
                    {
                        if (!assembly.Metadata.Units.Contains(id))
                        {
                            continue;
                        }
                    }

                    Log.Info($"Activate {assembly.Metadata.Name}");
                    assembly.Value.Activate();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private void DeactivatePlugins()
        {
            foreach (var assembly in this.Assemblies)
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

        private void OnExportsChanged(object sender, ExportsChangeEventArgs args)
        {
            this.ExportsChanged = true;
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

                UpdateManager.Subscribe(this.UpdateExportChange, 1000);
                this.Default.Container.ExportsChanged += this.OnExportsChanged;

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
            foreach (var assembly in this.Assemblies)
            {
                if (assembly.Metadata.Units == null)
                {
                    Log.Debug($"Found [{assembly.Metadata.Name}|{assembly.Metadata.Author}|{assembly.Metadata.Version}]");
                }
                else
                {
                    Log.Debug($"Found [{assembly.Metadata.Name}|{assembly.Metadata.Author}|{assembly.Metadata.Version}|{string.Join(", ", assembly.Metadata.Units)}]");
                }
            }
        }

        private void UpdateExportChange()
        {
            if (this.ExportsChanged)
            {
                this.ExportsChanged = false;
                this.ActivatePlugins();
            }
        }
    }
}