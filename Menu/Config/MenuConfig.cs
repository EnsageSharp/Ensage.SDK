// <copyright file="MenuConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
    using System.Collections.Generic;

    using Ensage.SDK.Menu.Items;

    [Menu("SDKMenu")]
    public class MenuConfig
    {
        public MenuConfig()
        {
            var args = new[]
            {
                new KeyValuePair<string, bool>("menuStyle/default/left", true), new KeyValuePair<string, bool>("menuStyle/default/right", false),
                new KeyValuePair<string, bool>("menuStyle/default/leftHover", false), new KeyValuePair<string, bool>("menuStyle/default/rightHover", true)
            };
            Picker = new PicturePicker(args);
        }

        [Menu("General Settings")]
        public GeneralConfig GeneralConfig { get; set; } = new GeneralConfig();

        [Menu("Hacks")]
        public HackConfig HackConfig { get; set; } = new HackConfig();

        [Item("Test Picker")]
        public PicturePicker Picker { get; set; }

        [Item]
        public string TestString { get; set; } = "hello world!";
    }
}