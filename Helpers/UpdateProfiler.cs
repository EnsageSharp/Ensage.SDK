// <copyright file="UpdateProfiler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Linq;

    using Ensage.SDK.EventHandler;

    using SharpDX;

    public class UpdateProfiler : IDisposable
    {
        private bool disposed;

        public UpdateProfiler()
        {
            Drawing.OnDraw += this.OnDraw;
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
                Drawing.OnDraw -= this.OnDraw;
            }

            this.disposed = true;
        }

        private void OnDraw(EventArgs args)
        {
            var x = 20;
            var y = 150;

            var color = Color.Aqua;
            var size = new Vector2(20);

            foreach (var handler in UpdateManager.PreUpdateHandlers.OfType<TraceUpdateHandler>())
            {
                Drawing.DrawText($"{handler.Time.Ticks:n0}", new Vector2(x, y), size, color, FontFlags.AntiAlias);
                Drawing.DrawText($"{handler}", new Vector2(x + 80, y), size, color, FontFlags.AntiAlias);
                y += 25;
            }

            foreach (var handler in UpdateManager.UpdateHandlers.OfType<TraceUpdateHandler>())
            {
                Drawing.DrawText($"{handler.Time.Ticks:n0}", new Vector2(x, y), size, color, FontFlags.AntiAlias);
                Drawing.DrawText($"{handler}", new Vector2(x + 80, y), size, color, FontFlags.AntiAlias);
                y += 25;
            }
        }
    }
}