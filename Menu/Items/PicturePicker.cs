using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ensage.SDK.Menu.Items
{
    public class PicturePicker : ILoadable
    {
        public Dictionary<string, bool> PictureStates { get; set; } = new Dictionary<string, bool>();

        public PicturePicker()
        {
            
        }

        public PicturePicker(bool defaultValue = true, params string[] textureKeys)
        {
            foreach (var textureKey in textureKeys)
            {
                this.PictureStates.Add(textureKey, defaultValue);
            }
        }

        public PicturePicker(params KeyValuePair<string, bool>[] values)
        {
            foreach(var value in values)
            {
                this.PictureStates.Add(value.Key, value.Value);
            }
        }

        public bool this[string key]
        {
            get
            {
                return this.PictureStates[key];
            }
            set
            {
                this.PictureStates[key] = value;
            }
        }


        public bool Load(object data)
        {
            var selection = (PicturePicker)data;

            if (PictureStates.Keys.SequenceEqual(selection.PictureStates.Keys))
            {
                PictureStates = selection.PictureStates;
                return true;
            }

            return false;
        }
    }
}
