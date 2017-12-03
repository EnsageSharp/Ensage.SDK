namespace Ensage.SDK.Menu.Config
{
    using System.ComponentModel;

    using Ensage.SDK.Menu.Attributes;

    public class HackConfig
    {
        [Item("Auto accept games")]
        [Tooltip("Auto accept games as soon as one was found")]
        [DefaultValue(true)]
        [PermaShow]
        public bool AutoAccept
        {
            get
            {
                return Ensage.Config.AutoAccept;
            }

            set
            {
                Ensage.Config.AutoAccept = value;
            }
        }

        [Item("Show spawn boxes")]
        [Tooltip("Spawn boxes of neutrals are permanently visible")]
        [DefaultValue(true)]
        public bool ShowSpawnBoxes
        {
            get
            {
                return Ensage.Config.ShowSpawnBoxes;
            }

            set
            {
                Ensage.Config.ShowSpawnBoxes = value;
            }
        }

        [Item("Show tower range")]
        [Tooltip("The range indicator of towers are permanently visible")]
        [DefaultValue(true)]
        public bool ShowTowerRange
        {
            get
            {
                return Ensage.Config.ShowTowerRange;
            }

            set
            {
                Ensage.Config.ShowTowerRange = value;
            }
        }
    }
}