// <copyright file="GeneralConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
    using System.ComponentModel;

    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Menu.Styles;

    public class GeneralConfig
    {
        [Item("Style")]
        public Selection<IMenuStyle> ActiveStyle { get; set; }

        [Item("Animation speed")]
        [Tooltip("How fast animations are played")]
        [DefaultValue(true)]
        public Slider AnimationTime { get; set; } = new Slider(3, 0, 5);

        [Item("Use menu animations")]
        [Tooltip("Enable different animations to the menu")]
        [DefaultValue(true)]
        public bool UseAnimations { get; set; }
    }
}