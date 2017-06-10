// <copyright file="TargetSelectorConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Config
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class TargetSelectorConfig : IDisposable
    {
        private readonly string[] staticModes =
            {
                "None",
                "Auto (Near Mouse)"
            };

        private bool disposed;

        public TargetSelectorConfig(IEnumerable<string> names)
        {
            var modes = this.staticModes.Concat(names).ToArray();

            this.Factory = MenuFactory.Create("Target Selector");
            this.Selection = this.Factory.Item("Active Selector", new StringList(modes, 1));
        }

        public MenuFactory Factory { get; }

        public MenuItem<StringList> Selection { get; }

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