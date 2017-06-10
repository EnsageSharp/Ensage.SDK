// <copyright file="OrbwalkerConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Config
{
    using System;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class OrbwalkerConfig : IDisposable
    {
        private bool disposed;

        public OrbwalkerConfig(string name, string[] orbwalkers)
        {
            this.Factory = MenuFactory.Create($"Orbwalker: {name}", "Orbwalker");
            this.Selection = this.Factory.Item("Active Orbwalker", new StringList(orbwalkers));
            this.TickRate = this.Factory.Item("Tickrate", new Slider(100, 0, 200));
        }

        public MenuFactory Factory { get; }

        public MenuItem<StringList> Selection { get; }

        public MenuItem<Slider> TickRate { get; }

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
                this.Factory.Dispose();
            }

            this.disposed = true;
        }
    }
}