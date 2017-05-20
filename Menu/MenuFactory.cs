// <copyright file="MenuFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Ensage.Common.Menu;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    public class MenuFactory : IDisposable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        static MenuFactory()
        {
            AppDomain.CurrentDomain.DomainUnload += OnDomainUnload;
        }

        public MenuFactory(Menu parent)
        {
            this.parent = parent;
        }

        public MenuFactory Parent
        {
            get
            {
                return Attach(this.parent.Parent);
            }
        }

        private static Dictionary<string, MenuItem> Items { get; } = new Dictionary<string, MenuItem>();

        private static Dictionary<string, Menu> Menus { get; } = new Dictionary<string, Menu>();

        private Menu parent { get; }

        public static MenuFactory Attach(Menu parent)
        {
            return new MenuFactory(parent);
        }

        public static MenuItem<T> Attach<T>(MenuItem item)
            where T : struct
        {
            return new MenuItem<T>(item);
        }

        public static MenuFactory Attach(string name)
        {
            return new MenuFactory(Common.Menu.Menu.GetMenu("Ensage.SDK", name));
        }

        public static MenuFactory Create(string displayName, string name = null)
        {
            name = name ?? GetName(displayName);

            if (!Menus.ContainsKey(name))
            {
                var menu = new Menu(displayName, name, true);
                menu.AddToMainMenu(Assembly.GetExecutingAssembly());

                Menus[name] = menu;
            }

            return Attach(Menus[name]);
        }

        public static void Save()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache", "game", "sdk.json");
            var data = new Dictionary<string, object>();

            foreach (var item in Items)
            {
                data[item.Key] = item.Value.GetValue<object>();
            }

            JsonFactory.ToFile(file, data);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public MenuItem<T> Item<T>(string displayName, T value)
            where T : struct
        {
            return this.Item<T>(displayName, null, value);
        }

        public MenuItem<T> Item<T>(string displayName, string name = null)
            where T : struct
        {
            name = $"{this.parent.Name}.{name ?? GetName(displayName)}";

            if (!Items.ContainsKey(name))
            {
                var item = new MenuItem<T>(displayName, name);
                this.parent.AddItem(item.Item);

                Items[name] = item.Item;
            }

            return Attach<T>(Items[name]);
        }

        public MenuItem<T> Item<T>(string displayName, string name, T value)
            where T : struct
        {
            name = $"{this.parent.Name}.{name ?? GetName(displayName)}";

            if (!Items.ContainsKey(name))
            {
                var item = new MenuItem<T>(displayName, name, value);
                this.parent.AddItem(item.Item);

                Items[name] = item.Item;
            }

            return Attach<T>(Items[name]);
        }

        public MenuFactory Menu(string displayName, string name = null)
        {
            name = $"{this.parent.Name}.{name ?? GetName(displayName)}";

            if (!Menus.ContainsKey(name))
            {
                var menu = new Menu(displayName, name);
                this.parent.AddSubMenu(menu);

                Menus[name] = menu;
            }

            return Attach(Menus[name]);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                Log.Debug($"Dispose {this.parent.Name}");
                this.parent.RemoveFromMainMenu(Assembly.GetExecutingAssembly());
            }

            this.disposed = true;
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