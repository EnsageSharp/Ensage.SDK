// <copyright file="ContainerFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class ContainerFactory
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static CatalogLoader loader;

        public static CatalogLoader Loader
        {
            get
            {
                if (loader == null)
                {
                    loader = new CatalogLoader();
                }

                return loader;
            }
        }

        public static ContextContainer<IServiceContext> CreateContainer(Hero owner, bool disableSilentRejection = true)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            var flags = CompositionOptions.IsThreadSafe;
            if (disableSilentRejection)
            {
                flags |= CompositionOptions.DisableSilentRejection;
            }

            var context = new EnsageServiceContext(owner);
            var container = new CompositionContainer(Loader.Catalog, flags);
            context.Container = new ContextContainer<IServiceContext>(context, container);

            Log.Debug($"Create {context} Container");
            Log.Debug($"====================================================");
            Log.Debug($"Resolving Catalogs {Loader.Catalog.Catalogs.Count}");
            Log.Debug($"====================================================");

            foreach (var catalog in Loader.Catalog.Catalogs.OfType<AssemblyCatalog>())
            {
                Log.Debug($"Assembly {catalog.Assembly.GetName().Name}");
            }

            Log.Debug($"====================================================");
            Log.Debug($"Resolving Parts {container.Catalog.Parts.Count()}");
            Log.Debug($"====================================================");

            foreach (var part in container.Catalog.Parts)
            {
                Log.Debug($"{part}");
            }

            Log.Debug($"====================================================");

            container.ComposeExportedValue<IServiceContext>(context);

            // switch (Drawing.RenderMode)
            // {
            // case RenderMode.Dx11:
            // container.ComposeExportedValue<ID2DContext>(new D2DContext());
            // container.ComposeExportedValue<ID2DRenderer>(new D2DRenderer());
            // break;

            // case RenderMode.Dx9:
            // case RenderMode.OpenGL:
            // case RenderMode.Vulkan:
            // throw new NotSupportedException($"RenderMode({Drawing.RenderMode}) not supported.");
            // }
            return context.Container;
        }
    }
}