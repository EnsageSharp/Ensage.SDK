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
            this.Settings = new SettingsConfig(this.Factory);
            this.Debug = new DebugConfig(this.Factory);
        }

        public DebugConfig Debug { get; }

        public MenuFactory Factory { get; }

        public SettingsConfig Settings { get; }

        public class DebugConfig
        {
            public DebugConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Debug");
            }

            public MenuFactory Factory { get; }
        }

        public class SettingsConfig
        {
            public SettingsConfig(MenuFactory parent)
            {
                this.Factory = parent.Menu("Settings");
            }

            public MenuFactory Factory { get; }
        }
    }
}