// <copyright file="UpdateProfiler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.EventHandler;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [Export(typeof(IUpdateProfiler))]
    public class UpdateProfiler : ControllableService, IUpdateProfiler, IDisposable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        [ImportingConstructor]
        public UpdateProfiler([Import] SDKConfig config)
            : base(config.Debug.Factory, "Enable Profiler", false, true)
        {
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
                this.OnDeactivate();
            }

            this.disposed = true;
        }

        protected override void OnActivate()
        {
            foreach (var handler in UpdateManager.Handlers)
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

            Drawing.OnDraw += this.OnDraw;
        }

        protected override void OnDeactivate()
        {
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
            var color = Color.Orange;
            var size = new Vector2(25);

            foreach (var handler in UpdateManager.Handlers.Where(e => e.Executor is TraceHandler))
            {
                var tracer = (TraceHandler)handler.Executor;

                Drawing.DrawText($"{tracer.Time.Ticks:n0}", new Vector2(x, y), size, color, FontFlags.AntiAlias);

                if (tracer.Timeout > 0)
                {
                    Drawing.DrawText($"{handler.Name} t#{tracer.Timeout}", new Vector2(x + 100, y), size, color, FontFlags.AntiAlias);
                }
                else
                {
                    Drawing.DrawText($"{handler.Name}", new Vector2(x + 100, y), size, color, FontFlags.AntiAlias);
                }
                
                y += 25;
            }
        }
    }
}