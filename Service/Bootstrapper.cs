// <copyright file="Bootstrapper.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.AppDomain.Loader;
    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    public class Bootstrapper : AssemblyEntryPoint
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportMany(typeof(IAssemblyLoader))]
        public IEnumerable<Lazy<IAssemblyLoader, IAssemblyLoaderMetadata>> Assemblies { get; private set; }

        public ContextContainer<IServiceContext> Default { get; private set; }

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
            Game.OnIngameUpdate += this.OnLoad;
        }

        protected override void OnDeactivated()
        {
            try
            {
                foreach (var assembly in this.Assemblies)
                {
                    if (assembly.IsValueCreated)
                    {
                        Log.Info($"Deactivate {assembly.Metadata.Name}");
                        assembly.Value.Deactivate();
                    }
                }

                Log.Debug($"Dispose Context({this.Default.Context}) Container");
                this.Default?.Dispose();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void OnLoad(EventArgs args)
        {
            if (ObjectManager.LocalHero == null)
            {
                return;
            }

            Game.OnIngameUpdate -= this.OnLoad;

            try
            {
                this.Default = ContainerFactory.CreateContainer(ObjectManager.LocalHero);
                this.Default.BuildUp(this);

                IoC.Initialize(this.BuildUp, this.GetInstance, this.GetAllInstances);

                foreach (var assembly in this.Assemblies)
                {
                    if (assembly.Metadata.Units == null)
                    {
                        Log.Debug($"Found {assembly.Metadata.Name}|{assembly.Metadata.Author}|{assembly.Metadata.Version}");
                    }
                    else
                    {
                        Log.Debug($"Found {assembly.Metadata.Name}|{assembly.Metadata.Author}|{assembly.Metadata.Version}|{string.Join(", ", assembly.Metadata.Units)}");
                    }
                }

                var id = ObjectManager.LocalHero.HeroId;

                foreach (var assembly in this.Assemblies)
                {
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