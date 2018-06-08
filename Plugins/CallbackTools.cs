// <copyright file="CallbackTools.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    

    using NLog;

    //[ExportPlugin("Callback Tools", StartupMode.Manual, priority: 1000, description: "Display UpdateManager callback times in CPU Ticks")]
    public class CallbackTools : Plugin
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected override void OnActivate()
        {
            UpdateManager.Subscribe(this.UpdateHandler, 1000);
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.UpdateHandler);

            foreach (var handler in UpdateManager.Handlers.Where(e => e.Executor is TraceHandler))
            {
                var tracer = (TraceHandler)handler.Executor;
                if (tracer.Timeout > 0)
                {
                    handler.Executor = new TimeoutHandler(tracer.Timeout);
                }
                else
                {
                    handler.Executor = InvokeHandler.Default;
                }

                Log.Debug($"Updated {handler}");
            }
        }

        private void UpdateHandler()
        {
            foreach (var handler in UpdateManager.Handlers.Where(e => !(e.Executor is TraceHandler)))
            {
                var timeout = 0;
                var timer = handler.Executor as TimeoutHandler;

                if (timer != null)
                {
                    timeout = timer.Timeout;
                }

                handler.Executor = new TraceHandler(timeout);
                Log.Debug($"Updated {handler}");
            }
        }
    }
}