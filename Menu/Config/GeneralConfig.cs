// <copyright file="GeneralConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
    using System.ComponentModel;

    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Menu.Styles.Elements;

    using Config = Ensage.Config;

    public class GeneralConfig
    {
        [Item("Style")]
        public Selection<IMenuStyle> ActiveStyle { get; set; }

        [Item("Discord RPC")]
        [Tooltip("Enables rich present of Discord")]
        [DefaultValue(true)]
        public bool Discord
        {
            get
            {
                return Config.Discord;
            }

            set
            {
                Config.Discord = value;
            }
        }

        [Item("Block player inputs for KeyBinds")]
        [Tooltip("When a assembly uses a key, dota will ignore it")]
        [DefaultValue(false)]
        public bool BlockKeys { get; set; }

        // TODO: some day
        //[Item("Animation speed")]
        //[Tooltip("How fast animations are played")]
        //public Slider AnimationTime { get; set; } = new Slider(3, 0, 5);

        //[Item("Use menu animations")]
        //[Tooltip("Enable different animations to the menu")]
        //[DefaultValue(true)]
        //public bool UseAnimations { get; set; }
    }
}