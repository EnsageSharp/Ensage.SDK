// <copyright file="OrbwalkerConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using Ensage.Common.Menu;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Service;

    public class OrbwalkerConfig
    {
        public OrbwalkerConfig(IServiceContext context)
        {
            this.Factory = MenuFactory.Create($"Orbwalker: {context.Owner.GetDisplayName()}", "Orbwalker");
            this.Settings = new OrbwalkerSettings(this.Factory);

            context.Container.RegisterValue(this);
        }

        public MenuFactory Factory { get; }

        public OrbwalkerSettings Settings { get; }

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
            }

            public MenuItem<bool> Attack { get; }

            public MenuItem<Slider> AttackDelay { get; }

            public MenuItem<bool> DrawRange { get; }

            public MenuFactory Factory { get; }

            public MenuItem<bool> Move { get; }

            public MenuItem<Slider> MoveDelay { get; }
        }
    }
}