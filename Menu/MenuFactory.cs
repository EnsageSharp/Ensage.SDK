// <copyright file="MenuFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Reflection;

    using Ensage.Common.Menu;

    public class MenuFactory : IDisposable
    {
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
            var menu = new Menu(displayName, $"{name ?? displayName}", true);
            menu.AddToMainMenu(Assembly.GetCallingAssembly());

            return new MenuFactory(menu);
        }

        public static MenuFactory Create(Menu parent, string displayName, string name = null)
        {
            var menu = new Menu(displayName, $"{parent.Name}.{name ?? displayName}");
            parent.AddSubMenu(menu);

            return new MenuFactory(menu);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public MenuItem<T> Item<T>(string displayName, T value)
        {
            var item = new MenuItem<T>(displayName, $"{this.Parent.Name}.{displayName}", value);
            this.Parent.AddItem(item.Item);

            return item;
        }

        public MenuItem<T> Item<T>(string displayName, string name, T value)
        {
            var item = new MenuItem<T>(displayName, $"{this.Parent.Name}.{name}", value);
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
                this.Parent.RemoveFromMainMenu(Assembly.GetCallingAssembly());
            }

            this.disposed = true;
        }
    }
}