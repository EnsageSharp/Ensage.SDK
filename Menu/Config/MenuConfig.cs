// <copyright file="MenuConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
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

        public Slider<Vector2> MenuPosition { get; set; } = new Slider<Vector2>(new Vector2(200, 50), new Vector2(0, 0), new Vector2(Drawing.Width, Drawing.Height));

        public Slider<Vector2> PermaPosition { get; set; } = new Slider<Vector2>(new Vector2(200, 50), new Vector2(0, 0), new Vector2(Drawing.Width, Drawing.Height));
    }
}