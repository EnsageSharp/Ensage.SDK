// <copyright file="CatalogLoader.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class CatalogLoader : IDisposable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

            Log.Debug($"Add Catalog {assembly.GetName().Name}");
            this.Catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            this.LoadedAssemblies.Add(assembly);
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

        private void OnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            this.Add(args.LoadedAssembly);
        }
    }
}