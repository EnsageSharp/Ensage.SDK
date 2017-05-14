// <copyright file="SamplePluginConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class SamplePluginConfig : IDisposable
    {
        public SamplePluginConfig()
        {
            this.Factory = MenuFactory.Create("Example Plugin");
            this.UseExploit = this.Factory.Item("Use Exploit", true);
            this.Key = this.Factory.Item("Exploit Key", new KeyBind('K'));
        }

        public MenuFactory Factory { get; }

        public MenuItem<KeyBind> Key { get; }

        public MenuItem<bool> UseExploit { get; }

        public void Dispose()
        {
            this.Factory?.Dispose();
        }
    }
}