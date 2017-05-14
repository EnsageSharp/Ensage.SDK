// <copyright file="DeveloperTools.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportPlugin("Developer Tools", StartupMode.Manual, priority: 1000, description: "Display UpdateManager callback times in CPU Ticks")]
    public class DeveloperTools : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnActivate()
        {
            Drawing.OnDraw += this.OnDraw;
            UpdateManager.Subscribe(this.UpdateHandler, 1000);
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.UpdateHandler);
            Drawing.OnDraw -= this.OnDraw;

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

        private void OnDraw(EventArgs args)
        {
            var x = 20;
            var y = 130;
            var flag = FontFlags.DropShadow | FontFlags.AntiAlias;
            var color = Color.Beige;
            var size = new Vector2(22);

            Drawing.DrawText($"Ticks", new Vector2(x, y), size, color, flag);
            Drawing.DrawText($"Average", new Vector2(x + 100, y), size, color, flag);
            Drawing.DrawText($"Min", new Vector2(x + 200, y), size, color, flag);
            Drawing.DrawText($"Max", new Vector2(x + 300, y), size, color, flag);
            Drawing.DrawText($"Time", new Vector2(x + 400, y), size, color, flag);
            Drawing.DrawText($"Name", new Vector2(x + 500, y), size, color, flag);
            y += 25;

            foreach (var handler in UpdateManager.Handlers.Where(e => e.Executor is TraceHandler))
            {
                var tracer = (TraceHandler)handler.Executor;

                Drawing.DrawText($"{tracer.Time.Ticks:n0}", new Vector2(x, y), size, color, flag);
                Drawing.DrawText($"{tracer.TimeHistory.Average(e => e.Ticks):n0}", new Vector2(x + 100, y), size, color, flag);
                Drawing.DrawText($"{tracer.TimeHistory.Min(e => e.Ticks):n0}", new Vector2(x + 200, y), size, color, flag);
                Drawing.DrawText($"{tracer.TimeHistory.Max(e => e.Ticks):n0}", new Vector2(x + 300, y), size, color, flag);
                Drawing.DrawText($"{tracer.Timeout}", new Vector2(x + 400, y), size, color, flag);
                Drawing.DrawText($"{handler.Name}", new Vector2(x + 500, y), size, color, flag);

                y += 25;
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