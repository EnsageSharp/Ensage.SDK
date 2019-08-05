// <copyright file="HackConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Config
{
    using System.ComponentModel;

    using Config = Ensage.Config;

    public class HackConfig
    {
        [Item("Auto accept games")]
        [Tooltip("Auto accept games as soon as one was found")]
        [DefaultValue(true)]
        public bool AutoAccept
        {
            get
            {
                return Config.AutoAccept;
            }

            set
            {
                Config.AutoAccept = value;
            }
        }

        [Item("Show spawn boxes")]
        [Tooltip("Spawn boxes of neutrals are permanently visible")]
        [DefaultValue(false)]
        public bool ShowSpawnBoxes
        {
            get
            {
                return Config.ShowSpawnBoxes;
            }

            set
            {
                Config.ShowSpawnBoxes = value;
            }
        }

        [Item("Show tower range")]
        [Tooltip("The range indicator of towers are permanently visible")]
        [DefaultValue(false)]
        public bool ShowTowerRange
        {
            get
            {
                return Config.ShowTowerRange;
            }

            set
            {
                Config.ShowTowerRange = value;
            }
        }
    }
}