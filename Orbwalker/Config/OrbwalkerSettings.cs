// <copyright file="OrbwalkerSettings.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Config
{
    using System;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class OrbwalkerSettings : IDisposable
    {
        private bool disposed;

        public OrbwalkerSettings(MenuFactory parent, Unit owner)
        {
            if (owner == ObjectManager.LocalHero)
            {
                this.Factory = parent.Menu($"Settings");
            }
            else if (owner.IsIllusion)
            {
                this.Factory = parent.Menu($"Settings (Illusion)");
            }
            else
            {
                this.Factory = parent.Menu($"Settings ({owner.Name})");
            }

            this.Move = this.Factory.Item("Move", true);
            this.Attack = this.Factory.Item("Attack", true);
            this.DrawRange = this.Factory.Item("Draw Attack Range", false);
            this.DrawHoldRange = this.Factory.Item("Draw Hold Range", false);

            this.MoveDelay = this.Factory.Item("Move Delay", new Slider(60, 0, 200));
            this.AttackDelay = this.Factory.Item("Attack Delay", new Slider(5, 0, 200));
            this.TurnDelay = this.Factory.Item("Turn Delay", new Slider(100, -200, 200));

            this.HoldRange = this.Factory.Item("Hold Position Range", new Slider(60, 0, 300));
        }

        public MenuItem<bool> Attack { get; }

        public MenuItem<Slider> AttackDelay { get; }

        public MenuItem<bool> DrawHoldRange { get; }

        public MenuItem<bool> DrawRange { get; }

        public MenuFactory Factory { get; }

        public MenuItem<Slider> HoldRange { get; }

        public MenuItem<bool> Move { get; }

        public MenuItem<Slider> MoveDelay { get; }

        public MenuItem<Slider> TurnDelay { get; }

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