// <copyright file="OrbwalkerConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System.ComponentModel.Composition;

    using Ensage.Common.Menu;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Service;

    [Export]
    public class OrbwalkerConfig
    {
        [ImportingConstructor]
        public OrbwalkerConfig(IServiceContext context)
        {
            this.Factory = MenuFactory.Create($"Orbwalker: {context.Owner.GetDisplayName()}", "Orbwalker");
            this.Settings = new OrbwalkerSettings(this.Factory);
        }

        public MenuFactory Factory { get; }

        public OrbwalkerSettings Settings { get; }

        public class OrbwalkerSettings
        {
            public OrbwalkerSettings(MenuFactory parent)
            {
                this.Factory = parent.Menu("Settings");

                this.Attack = this.Factory.Item("Attack", true);
                this.Move = this.Factory.Item("Move", true);
                this.MoveLimit = this.Factory.Item("Move Limit", new Slider(60, 0, 250));
            }

            public MenuItem<bool> Attack { get; set; }

            public MenuFactory Factory { get; }

            public MenuItem<bool> Move { get; set; }

            public MenuItem<Slider> MoveLimit { get; set; }
        }
    }
}