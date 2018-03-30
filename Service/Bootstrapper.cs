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
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Logger;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.VPK;

    using NLog;

    using PlaySharp.Toolkit.AppDomain.Loader;
    using PlaySharp.Toolkit.Helper;

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
                if (this.Context.menuManager.IsValueCreated)
                {
                    this.Context.MenuManager.Dispose();
                }

                if (this.Context.rendererManager.IsValueCreated)
                {
                    this.Context.Renderer.Dispose();
                }
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
        }

        private void ActivatePlugins()
        {
            foreach (var plugin in this.PluginContainer.OrderBy(e => e.Metadata.Delay))
            {
                if (plugin.Menu && !plugin.IsActive)
                {
                    UpdateManager.BeginInvoke(plugin.Activate, plugin.Metadata.Delay);
                }
            }
        }

        private async Task ActivatePluginsTask()
        {
            var startTime = Stopwatch.StartNew();
            var activationTime = new Stopwatch();

            var currentFrame = UpdateManager.Frame;
            var frameTime = new Stopwatch();

            foreach (var plugin in this.PluginContainer.Where(e => e.Menu && !e.IsActive).OrderBy(e => e.Metadata.Delay))
            {
                if (currentFrame != UpdateManager.Frame)
                {
                    frameTime.Restart();
                    currentFrame = UpdateManager.Frame;
                }

                if (startTime.ElapsedMilliseconds > plugin.Metadata.Delay)
                {
                    activationTime.Restart();
                    plugin.Activate();
                    activationTime.Stop();

                    Log.Info($"Activated Plugin {plugin.Metadata.Name} in {activationTime.Elapsed}");

                    if (frameTime.ElapsedMilliseconds > 100)
                    {
                        Log.Trace($"Delaying Plugin Activation {frameTime.Elapsed}");
                        await Task.Delay(250);
                    }
                }

                await Task.Delay(100);
            }
        }

        private async Task ActivateServices()
        {
            var actions = new Action[]
            {
                () =>
                {
                    Log.Debug($"{Thread.CurrentThread.ManagedThreadId} VpkBrowser");
                    this.Default.Get<VpkBrowser>();
                },
                //() =>
                //{
                //    Log.Debug($"{Thread.CurrentThread.ManagedThreadId} TextureManager");
                //    var textures = this.Context.TextureManager;
                //},
                //() =>
                //{
                //    Log.Debug($"{Thread.CurrentThread.ManagedThreadId} Renderer");
                //    var renderer = this.Context.Renderer;
                //},
                //() =>
                //{
                //    Log.Debug($"{Thread.CurrentThread.ManagedThreadId} MenuManager");
                //    var menu = this.Context.MenuManager;
                //},
                () =>
                {
                    Log.Debug($"{Thread.CurrentThread.ManagedThreadId} Maps");
                    var maps = this.Context.GetAll<Map>();
                }
            };

            Log.Trace($"ActivateServices {Thread.CurrentThread.ManagedThreadId}");
            await Task.WhenAll(actions.Select(Task.Run).ToArray());
        }

        private void DeactivatePlugins()
        {
            foreach (var plugin in this.PluginContainer.OrderByDescending(e => e.Metadata.Delay))
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

        private void AddPlugin(Lazy<IPluginLoader, IPluginLoaderMetadata> assembly)
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

        private void DiscoverPlugins()
        {
            foreach (var assembly in this.Plugins.OrderBy(e => e.Metadata.Delay))
            {
                this.AddPlugin(assembly);
            }
        }

        private async void OnBootstrap()
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

                Log.Debug($">> Searching Plugins");
                this.DiscoverPlugins();

                sw.Stop();

                Log.Debug("====================================================");
                Log.Debug($">> Bootstrap completed in {sw.Elapsed}");
                Log.Debug("====================================================");

                Log.Debug($">> Activating Services");
                await Task.Delay(500);
                await this.ActivateServices();

                Log.Debug($">> Activating Plugins");
                await Task.Delay(500);
                await this.ActivatePluginsTask();

                ContainerFactory.Loader.AssemblyLoad += this.OnAssemblyLoad;
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

        private void OnAssemblyLoad(object sender, Assembly assembly)
        {
            foreach (var plugin in this.Plugins.Where(e => e.Value.GetType().Assembly == assembly))
            {
                this.AddPlugin(plugin);
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