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

        public static ContextContainer<IServiceContext> CreateContainer(IServiceContext context, bool disableSilentRejection = true)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Log.Debug($"Create Context({context}) Container");

            var flags = CompositionOptions.IsThreadSafe;
            if (disableSilentRejection)
            {
                flags |= CompositionOptions.DisableSilentRejection;
            }

            var container = new CompositionContainer(Loader.Catalog, flags);
            var cc = new ContextContainer<IServiceContext>(context, container);

            container.ComposeExportedValue(context);
            container.ComposeExportedValue(cc);

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
            return cc;
        }
    }
}