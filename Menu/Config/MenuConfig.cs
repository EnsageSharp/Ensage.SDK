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

        [Item]
        public HotkeyPressSelector HotkeyPress1 { get; set; } = new HotkeyPressSelector(Key.B, TestPress, HotkeyFlags.Press);

        [Item]
        public HotkeyPressSelector HotkeyPress2 { get; set; } = new HotkeyPressSelector(Key.V, TestPress, HotkeyFlags.Down);

        [Item]
        public HotkeyPressSelector HotkeyPress3 { get; set; } = new HotkeyPressSelector(Key.C, TestPress, HotkeyFlags.Up);


        [Item]
        public HotkeyPressSelector HotkeyPress5 { get; set; } = new HotkeyPressSelector(MouseButtons.Left, TestPress, HotkeyFlags.Down);

        [Item]
        public HotkeyPressSelector HotkeyPress6 { get; set; } = new HotkeyPressSelector(MouseButtons.Left, TestPress, HotkeyFlags.Up);


        private static void TestPress(MenuInputEventArgs args)
        {
            Console.WriteLine($"im pressed lul: {args.Key} | {args.MouseButton} > {args.Flag}");
        }
    }
}