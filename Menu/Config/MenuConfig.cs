// <copyright file="MenuConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [Menu("SDKMenu"), TexureAttribute("menuStyle/logo")]
    public class MenuConfig
    {
        public MenuConfig()
        {
        }

        [Menu("General Settings")]
        public GeneralConfig GeneralConfig { get; set; } = new GeneralConfig();

        [Menu("Hacks")]
        public HackConfig HackConfig { get; set; } = new HackConfig();

        public Vector2 MenuPosition { get; set; } = new Vector2(200, 50);

        public Vector2 PermaPosition { get; set; } = new Vector2(200, 50);
    }
}