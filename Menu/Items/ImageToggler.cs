// <copyright file="ImageToggler.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Helpers;

    public class UpdateMenuItemMessage
    {
        public UpdateMenuItemMessage(object instance)
        {
            this.Instance = instance;
        }

        public object Instance { get; }
    }

    public class ImageToggler : ILoadable, ICloneable
    {
        public ImageToggler()
        {
        }

        public ImageToggler(bool defaultValue = true, params string[] textureKeys)
        {
            foreach (var textureKey in textureKeys)
            {
                this.PictureStates.Add(textureKey, defaultValue);
            }
        }

        public ImageToggler(params KeyValuePair<string, bool>[] values)
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

        public virtual bool AddImage(bool state, params string[] textureKey)
        {
            var changed = false;
            foreach (var s in textureKey)
            {
                if (!this.PictureStates.ContainsKey(s))
                {
                    this.PictureStates.Add(s, state);
                    changed = true;
                }
            }

            if (changed)
            {
                Messenger<UpdateMenuItemMessage>.Publish(new UpdateMenuItemMessage(this));
            }

            return changed;
        }

        public virtual bool AddImage(params KeyValuePair<string, bool>[] values)
        {
            var changed = false;
            foreach (var pair in values)
            {
                if (!this.PictureStates.ContainsKey(pair.Key))
                {
                    this.PictureStates.Add(pair.Key, pair.Value);
                    changed = true;
                }
            }

            if (changed)
            {
                Messenger<UpdateMenuItemMessage>.Publish(new UpdateMenuItemMessage(this));
            }

            return changed;
        }

        public virtual object Clone()
        {
            var result = (ImageToggler)this.MemberwiseClone();
            result.PictureStates = new Dictionary<string, bool>(this.PictureStates);
            return result;
        }

        public virtual bool Load(object data)
        {
            var selection = (ImageToggler)data;
            if (selection.PictureStates == null)
            {
                return false;
            }

            // if (this.PictureStates.Keys.SequenceEqual(selection.PictureStates.Keys))
            if (this.PictureStates.Keys.All(x => selection.PictureStates.ContainsKey(x)))
            {
                // this.PictureStates = selection.PictureStates;
                this.PictureStates = selection.PictureStates.Where(x => this.PictureStates.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value);
                return true;
            }

            return false;
        }

        public virtual bool RemoveImage(string textureKey)
        {
            if (this.PictureStates.Remove(textureKey))
            {
                Messenger<UpdateMenuItemMessage>.Publish(new UpdateMenuItemMessage(this));
                return true;
            }

            return false;
        }
    }
}