using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.SDK.Menu.Items
{
    public class PriorityChanger : PicturePicker
    {
        public PriorityChanger()
        {
            
        }

        public PriorityChanger(bool defaultValue = false, params string[] textureKeys)
            : base(defaultValue, textureKeys)
        {
            Selectable = true;
        }

        public PriorityChanger(params KeyValuePair<string, bool>[] values)
            : base(values)
        {
            Selectable = true;
        }

        public PriorityChanger(params string[] textureKeys)
            : base(false, textureKeys)
        {
            Selectable = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the items can be selected and unselected.
        /// </summary>
        public bool Selectable { get; set; }

        public new bool Load(object data)
        {
            if (base.Load(data))
            {
                Selectable = ((PriorityChanger)data).Selectable;
                return true;
            }

            return false;
        }
    }
}
