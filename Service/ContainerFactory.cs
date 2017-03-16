// <copyright file="ContainerFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class ContainerFactory
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static AggregateCatalog _catalog;

        public static AggregateCatalog Catalog
        {
            get
            {
                if (_catalog == null)
                {
                    _catalog = new AggregateCatalog();
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        Log.Debug($"Add Catalog {assembly.GetName().Name}");
                        _catalog.Catalogs.Add(new AssemblyCatalog(assembly));
                    }
                }

                return _catalog;
            }

            private set
            {
                _catalog = value;
            }
        }

        public static ContextContainer<IEnsageServiceContext> CreateContainer(IEnsageServiceContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Log.Debug($"Create Context {context} Container");

            var container = new CompositionContainer(
                Catalog,
                CompositionOptions.IsThreadSafe | CompositionOptions.DisableSilentRejection);

            container.ComposeExportedValue(context);

            return new ContextContainer<IEnsageServiceContext>(context, container);
        }
    }
}