// <copyright file="MenuConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
    using System.Collections.Generic;

    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [Menu("SDKMenu")]
    public class MenuConfig
    {
        public MenuConfig()
        {
            var args = new[]
                           {
                               new KeyValuePair<string, bool>("menuStyle/default/left", true),
                               new KeyValuePair<string, bool>("menuStyle/default/right", false),
                               new KeyValuePair<string, bool>("menuStyle/default/leftHover", false),
                               new KeyValuePair<string, bool>("menuStyle/default/rightHover", true)
                           };
            this.Picker = new PicturePicker(args);

            this.PriorityChanger = new PriorityChanger(args);
        }

        [Menu("General Settings")]
        public GeneralConfig GeneralConfig { get; set; } = new GeneralConfig();

        [Menu("Hacks")]
        public HackConfig HackConfig { get; set; } = new HackConfig();

        public Vector2 MenuPosition { get; set; } = new Vector2(200, 50);

        [Item("Test Picker")]
        public PicturePicker Picker { get; set; }

        [Item("Test PriorityChanger")]
        public PriorityChanger PriorityChanger { get; set; }

        [Item]
        public string TestString { get; set; } = "hello world!";
    }
}