// <copyright file="MenuConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
    using System.ComponentModel;

    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [Menu("SDKMenu"), TextureResource("menuStyle/logo", @"MenuStyle.logo.png")]
    public class MenuConfig
    {
        public MenuConfig()
        {
        }

        [Menu("General Settings")]
        public GeneralConfig GeneralConfig { get; set; } = new GeneralConfig();

        [Menu("Hacks")]
        public HackConfig HackConfig { get; set; } = new HackConfig();

        [Item("PermaShow Active")]
        [Tooltip("Toggle menu items which will be shown, even when the menu is hidden")]
        [DefaultValue(true)]
        public bool IsPermaShowActive { get; set; }

        [Item("Menu Position")]
        public Slider<Vector2> MenuPosition { get; set; } = new Slider<Vector2>(new Vector2(200, 50), new Vector2(0, 0), new Vector2(Drawing.Width - 10, Drawing.Height - 10));

        [Item("Perma Menu Position")]
        public Slider<Vector2> PermaPosition { get; set; } = new Slider<Vector2>(new Vector2(200, 50), new Vector2(0, 0), new Vector2(Drawing.Width - 10, Drawing.Height - 10));
    }
}