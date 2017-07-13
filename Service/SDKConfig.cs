// <copyright file="SDKConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using Ensage.SDK.Menu;

    public class SDKConfig
    {
        public SDKConfig()
        {
            this.Factory = MenuFactory.Create("Ensage.SDK");
            this.Plugins = new PluginsConfig(this.Factory);
            this.Settings = new SettingsConfig(this.Factory);
            this.Debug = new DebugConfig(this.Factory);
        }

        public DebugConfig Debug { get; }

        public MenuFactory Factory { get; }

        public PluginsConfig Plugins { get; }

        public SettingsConfig Settings { get; }

        public class DebugConfig
        {
            public DebugConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Debug");
                this.ErrorReporting = this.Factory.Item("Error Reporting", true);
            }

            public MenuItem<bool> ErrorReporting { get; }

            public MenuFactory Factory { get; }
        }

        public class PluginsConfig
        {
            public PluginsConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Plugins");
            }

            public MenuFactory Factory { get; }
        }

        public class SettingsConfig
        {
            public SettingsConfig(MenuFactory parent)
            {
                // this.Factory = parent.Menu("Settings");
            }

            public MenuFactory Factory { get; }
        }
    }
}