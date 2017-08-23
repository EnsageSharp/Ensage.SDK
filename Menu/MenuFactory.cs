// <copyright file="MenuFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;

    using PlaySharp.Toolkit.Helper;

    public sealed class MenuFactory : IDisposable
    {
        static MenuFactory()
        {
            AppDomain.CurrentDomain.DomainUnload += OnDomainUnload;
        }

        public MenuFactory(Menu target)
        {
            this.Target = target;
        }

        public MenuFactory Parent
        {
            get
            {
                return Attach(this.Target.Parent);
            }
        }

        public Menu Target { get; }

        private static Dictionary<string, object> Items { get; } = new Dictionary<string, object>();

        private static Dictionary<string, MenuFactory> Menus { get; } = new Dictionary<string, MenuFactory>();

        public static MenuFactory Attach(Menu target)
        {
            return new MenuFactory(target);
        }

        public static MenuItem<T> Attach<T>(MenuItem item)
            where T : struct
        {
            return new MenuItem<T>(item);
        }

        public static MenuFactory Attach(string name)
        {
            return new MenuFactory(Common.Menu.Menu.GetMenu(Assembly.GetExecutingAssembly().GetName().Name, name));
        }

        public static MenuFactory Create(string displayName, string name = null)
        {
            name = name ?? GetName(displayName);

            if (!Menus.ContainsKey(name))
            {
                Menus[name] = Attach(new Menu(displayName, name, true));
            }

            var menu = Menus[name];

            if (!Common.Menu.Menu.RootMenus.ContainsValue(menu.Target))
            {
                menu.Target.AddToMainMenu(Assembly.GetExecutingAssembly());
            }

            return menu;
        }

        public static MenuFactory CreateWithTexture(string displayName, string textureName)
        {
            return CreateWithTexture(displayName, null, textureName);
        }

        public static MenuFactory CreateWithTexture(string displayName, string name, string textureName)
        {
            var menu = Create(displayName, name);
            menu.Target.TextureName = textureName;
            menu.Target.ShowTextWithTexture = true;

            return menu;
        }

        public static void Save()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache", "game", "sdk.json");
            var data = new Dictionary<string, object>();

            foreach (var item in Items)
            {
                data[item.Key] = ((dynamic)item.Value).Value;
            }

            JsonFactory.ToFile(file, data);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T GetValue<T>(string itemName)
        {
            return this.Target.Items.First(e => e.DisplayName == itemName).GetValue<T>();
        }

        public MenuItem<T> Item<T>(string displayName, T value)
            where T : struct
        {
            return this.Item(displayName, null, value);
        }

        public MenuItem<T> Item<T>(string displayName, string name = null)
            where T : struct
        {
            name = $"{this.Target.Name}.{name ?? GetName(displayName)}";

            if (!Items.ContainsKey(name))
            {
                Items[name] = new MenuItem<T>(displayName, name);
            }

            var item = (MenuItem<T>)Items[name];

            if (!this.Target.Items.Contains(item.Item))
            {
                this.Target.AddItem(item.Item);
            }

            return item;
        }

        public MenuItem<T> Item<T>(string displayName, string name, T value)
            where T : struct
        {
            name = $"{this.Target.Name}.{name ?? GetName(displayName)}";

            if (!Items.ContainsKey(name))
            {
                Items[name] = new MenuItem<T>(displayName, name, value);
            }

            var item = (MenuItem<T>)Items[name];

            if (!this.Target.Items.Contains(item.Item))
            {
                this.Target.AddItem(item.Item);
            }

            return item;
        }

        public MenuFactory Menu(string displayName, string name = null)
        {
            name = $"{this.Target.Name}.{name ?? GetName(displayName)}";

            if (!Menus.ContainsKey(name))
            {
                Menus[name] = Attach(new Menu(displayName, name));
            }

            var menu = Menus[name];

            if (!this.Target.Children.Contains(menu.Target))
            {
                this.Target.AddSubMenu(menu.Target);
            }

            return menu;
        }

        public MenuFactory MenuWithTexture(string displayName, string textureName)
        {
            return this.MenuWithTexture(displayName, null, textureName);
        }

        public MenuFactory MenuWithTexture(string displayName, string name, string textureName)
        {
            var menu = this.Menu(displayName, name);
            menu.Target.TextureName = textureName;
            menu.Target.ShowTextWithTexture = true;

            return menu;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Target.IsRootMenu)
                {
                    this.Target.RemoveFromMainMenu(Assembly.GetExecutingAssembly());
                }
                else
                {
                    this.Parent.Target.RemoveSubMenu(this.Target.Name);
                }
            }
        }

        private static string GetName(string displayName)
        {
            displayName = displayName.Replace(" ", string.Empty);
            return displayName;
        }

        private static void OnDomainUnload(object sender, EventArgs args)
        {
            Save();
        }
    }
}