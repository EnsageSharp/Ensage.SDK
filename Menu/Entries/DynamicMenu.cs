// <copyright file="DynamicMenu.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Helpers;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class AddDynamicMenuMessage
    {
        public AddDynamicMenuMessage(DynamicMenu dynamicMenu, string key, string name, [CanBeNull] string textureKey, object instance, bool isItem = false)
        {
            this.DynamicMenu = dynamicMenu;
            this.Key = key;
            this.Name = name;
            this.TextureKey = textureKey;
            this.Instance = instance;
            this.IsItem = isItem;
        }

        public DynamicMenu DynamicMenu { get; }

        public string Key { get; }

        public object Instance { get; }

        public bool IsItem { get; }

        public string Name { get; }

        [CanBeNull]
        public string TextureKey { get; }
    }

    public class DynamicMenuRemoveMessage
    {
        public DynamicMenuRemoveMessage(DynamicMenu dynamicMenu, string key)
        {
            this.DynamicMenu = dynamicMenu;
            this.Key = key;
        }

        public DynamicMenu DynamicMenu { get; }

        public string Key { get; }
    }

    [PublicAPI]
    public class DynamicMenu
    {
        public readonly Dictionary<string, object> ValueStorage = new Dictionary<string, object>();

        [JsonIgnore]
        public JToken LoadToken { get; set; }

        public void AddMenu(string key, string name, [CanBeNull] string textureKey, object instance)
        {
            if (this.ValueStorage.ContainsKey(key))
            {
                throw new Exception($"{key} already in value storage of {this.GetType().Name}");
            }

            this.ValueStorage[key] = instance;
            Messenger<AddDynamicMenuMessage>.Publish(new AddDynamicMenuMessage(this, key, name, textureKey, instance));
        }

        public void AddMenuItem(string key, string name, [CanBeNull] string textureKey, object instance)
        {
            if (this.ValueStorage.ContainsKey(key))
            {
                throw new Exception($"{key} already in value storage of {this.GetType().Name}");
            }

            this.ValueStorage[key] = instance;
            Messenger<AddDynamicMenuMessage>.Publish(new AddDynamicMenuMessage(this, key, name, textureKey, instance, true));
        }


        public void RemoveMenu(string key)
        {
             Messenger<DynamicMenuRemoveMessage>.Publish(new DynamicMenuRemoveMessage(this, key));
        }

        public void RemoveMenuItem(string key)
        {
            Messenger<DynamicMenuRemoveMessage>.Publish(new DynamicMenuRemoveMessage(this, key));
        }
    }
}