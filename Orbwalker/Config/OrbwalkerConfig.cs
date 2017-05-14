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
            this.Settings = new OrbwalkerSettings(this.Factory);
        }

        public MenuFactory Factory { get; }

        public MenuItem<StringList> Selection { get; }

        public OrbwalkerSettings Settings { get; }

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

        public class OrbwalkerSettings
        {
            public OrbwalkerSettings(MenuFactory parent)
            {
                this.Factory = parent.Menu("Settings");

                this.Move = this.Factory.Item("Move", true);
                this.Attack = this.Factory.Item("Attack", true);
                this.DrawRange = this.Factory.Item("Draw Attack Range", true);

                this.MoveDelay = this.Factory.Item("Move Delay", new Slider(5, 0, 250));
                this.AttackDelay = this.Factory.Item("Attack Delay", new Slider(5, 0, 250));
                this.TurnDelay = this.Factory.Item("Turn Delay", new Slider(100, -250, 250));
            }

            public MenuItem<bool> Attack { get; }

            public MenuItem<Slider> AttackDelay { get; }

            public MenuItem<bool> DrawRange { get; }

            public MenuFactory Factory { get; }

            public MenuItem<bool> Move { get; }

            public MenuItem<Slider> MoveDelay { get; }

            public MenuItem<Slider> TurnDelay { get; }
        }
    }
}