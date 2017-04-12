// <copyright file="MenuFactory.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using Ensage.Common.Menu;

    public class MenuFactory
    {
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
            var menu = new Menu(displayName, $"{Config.AppName}.{name ?? displayName}", true);
            menu.AddToMainMenu();

            return new MenuFactory(menu);
        }

        public static MenuFactory Create(Menu parent, string displayName, string name = null)
        {
            var menu = new Menu(displayName, $"{parent.Name}.{name ?? displayName}");
            parent.AddSubMenu(menu);

            return new MenuFactory(menu);
        }

        public MenuItem<T> Item<T>(string displayName, T value)
        {
            var item = new MenuItem<T>($"{this.Parent.Name}.{displayName}", displayName, value);
            this.Parent.AddItem(item.Item);

            return item;
        }

        public MenuItem<T> Item<T>(string displayName, string name, T value)
        {
            var item = new MenuItem<T>($"{this.Parent.Name}.{name}", displayName, value);
            this.Parent.AddItem(item.Item);

            return item;
        }

        public MenuFactory Menu(string displayName, string name = null)
        {
            return Create(this.Parent, displayName, name ?? displayName);
        }
    }
}