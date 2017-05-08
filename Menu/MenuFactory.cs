// <copyright file="MenuFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class MenuFactory : IDisposable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        public MenuFactory(Menu parent)
        {
            this.Parent = parent;
        }

        public Menu Parent { get; }

        public static MenuFactory Attach(Menu parent)
        {
            return new MenuFactory(parent);
        }

        public static MenuFactory Create(string displayName, string name = null)
        {
            var menu = new Menu(displayName, $"{name ?? GetName(displayName)}", true);
            menu.AddToMainMenu(Assembly.GetExecutingAssembly());

            Log.Debug($"Created {menu.Name}");

            return new MenuFactory(menu);
        }

        public static MenuFactory Create(Menu parent, string displayName, string name = null)
        {
            var menu = new Menu(displayName, $"{parent.Name}.{name ?? GetName(displayName)}");
            parent.AddSubMenu(menu);

            Log.Debug($"Created {menu.Name}");

            return new MenuFactory(menu);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public MenuItem<T> Item<T>(string displayName)
            where T : struct
        {
            return this.Item<T>(displayName, GetName(displayName));
        }

        public MenuItem<T> Item<T>(string displayName, T value)
            where T : struct
        {
            return this.Item<T>(displayName, GetName(displayName), value);
        }

        public MenuItem<T> Item<T>(string displayName, string name)
            where T : struct
        {
            var ns = $"{this.Parent.Name}.{name}";
            var menuItem = this.Parent.Items.FirstOrDefault(e => e.Name == ns);

            if (menuItem != null)
            {
                Log.Debug($"Attached {menuItem.Name}");
                return new MenuItem<T>(menuItem);
            }

            var item = new MenuItem<T>(displayName, ns);
            this.Parent.AddItem(item.Item);

            return item;
        }

        public MenuItem<T> Item<T>(string displayName, string name, T value)
            where T : struct
        {
            var ns = $"{this.Parent.Name}.{name}";
            var menuItem = this.Parent.Items.FirstOrDefault(e => e.Name == ns);

            if (menuItem != null)
            {
                Log.Debug($"Attached {menuItem.Name}");
                return new MenuItem<T>(menuItem);
            }

            var item = new MenuItem<T>(displayName, ns, value);
            this.Parent.AddItem(item.Item);

            return item;
        }

        public MenuFactory Menu(string displayName, string name = null)
        {
            return Create(this.Parent, displayName, name ?? displayName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                Log.Debug($"Dispose {this.Parent.Name}");
                this.Parent.RemoveFromMainMenu(Assembly.GetExecutingAssembly());
            }

            this.disposed = true;
        }

        private static string GetName(string displayName)
        {
            displayName = displayName.Replace(" ", string.Empty);
            return displayName;
        }
    }
}