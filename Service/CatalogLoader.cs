// <copyright file="CatalogLoader.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;

    using NLog;

    public class CatalogLoader : IDisposable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly string[] blacklist =
        {
            "PlaySharp.Service",
            "Newtonsoft.Json",
            "clipper_library",
            "System.Collections.Immutable",
            "SharpDX",
            "SharpDX.Direct2D1",
            "SharpDX.Direct3D9",
            "SharpDX.Direct3D11",
            "SharpDX.SharpDX",
            "SharpDX.DXGI",
            "SharpDX.Mathematics",
            "Ensage",
            "EnsageSharp.Sandbox",
            "Ensage.Common",
            "Ability.Core",
            "Anonymously Hosted DynamicMethods Assembly"
        };

        private bool disposed;

        public CatalogLoader()
        {
            AppDomain.CurrentDomain.AssemblyLoad += this.OnAssemblyLoad;

            this.Add(Assembly.GetExecutingAssembly());

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                this.Add(assembly);
            }
        }

        public event EventHandler<Assembly> AssemblyLoad;

        public AggregateCatalog Catalog { get; } = new AggregateCatalog();

        private List<Assembly> LoadedAssemblies { get; } = new List<Assembly>();

        public void Add(Assembly assembly)
        {
            if (assembly.ReflectionOnly)
            {
                return;
            }

            if (assembly.GlobalAssemblyCache)
            {
                return;
            }

            if (this.LoadedAssemblies.Contains(assembly))
            {
                return;
            }

            var name = assembly.GetName().Name;

            if (this.blacklist.Contains(name))
            {
                return;
            }

            if (name.StartsWith("MetadataViewProxies_"))
            {
                return;
            }

            if (!this.CanLoad(assembly))
            {
                Log.Error($"Failed to resolve dependencies for {name}");
                return;
            }

            Log.Debug($"Create Catalog {name}");
            this.Catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            this.LoadedAssemblies.Add(assembly);

            this.AssemblyLoad?.Invoke(this, assembly);
        }

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
                AppDomain.CurrentDomain.AssemblyLoad -= this.OnAssemblyLoad;
            }

            this.disposed = true;
        }

        private bool CanLoad(Assembly assemblyToLoad)
        {
            try
            {
                foreach (var name in assemblyToLoad.GetReferencedAssemblies())
                {
                    AppDomain.CurrentDomain.Load(name);
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Warn(e);
                return false;
            }
        }

        private void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            this.Add(args.LoadedAssembly);
        }
    }
}