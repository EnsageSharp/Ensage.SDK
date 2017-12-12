// <copyright file="PicturePicker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System.Collections.Generic;
    using System.Linq;

    public class PicturePicker : ILoadable
    {
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
            foreach (var value in values)
            {
                this.PictureStates.Add(value.Key, value.Value);
            }
        }

        public Dictionary<string, bool> PictureStates { get; set; } = new Dictionary<string, bool>();

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

        public virtual bool Load(object data)
        {
            var selection = (PicturePicker)data;

            if (this.PictureStates.Keys.SequenceEqual(selection.PictureStates.Keys))
            {
                this.PictureStates = selection.PictureStates;
                return true;
            }

            return false;
        }
    }
}