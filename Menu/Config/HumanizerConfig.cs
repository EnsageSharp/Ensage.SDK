using Ensage.SDK.Menu.Items;

namespace Ensage.SDK.Menu.Config
{
    using System.ComponentModel;

    using Config = Ensage.Config;

    public class HumanizerConfig
    {
        public HumanizerConfig()
        {
            QueueSize.ValueChanging += QueueSize_ValueChanging;
        }

        private void QueueSize_ValueChanging(object sender, ValueBinding.ValueChangingEventArgs<int> e)
        {
            Config.HumanizerQueueSize = e.Value;
        }

        [Item("Enabled")]
        [Tooltip("Enable built-in humanizer")]
        [DefaultValue(true)]
        public bool HumanizerEnabled
        {
            get
            {
                return Config.HumanizerEnabled;
            }

            set
            {
                Config.HumanizerEnabled = value;
            }
        }

        [Item("Auto-Selection")]
        [Tooltip("Ensures that all entities which receive orders are getting selected before")]
        [DefaultValue(true)]
        public bool HumanizerSelecting
        {
            get
            {
                return Config.HumanizerSelecting;
            }

            set
            {
                Config.HumanizerSelecting = value;
            }
        }

        [Item("Queue Size")]
        [Tooltip("Internal queue size for orders. Higher values can cause more order lag.")]
        public Slider<int> QueueSize { get; set; } = new Slider<int>(8, 1, 32);

    }
}
