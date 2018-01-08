// <copyright file="MenuAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class MenuAttribute : Attribute
    {
        public MenuAttribute(string name = null)
        {
            this.Name = name;
        }

        public string Name { get; }
    }

    // public class SDKDynamicMenu
    // {
    // private readonly List<MenuItem> menuItems = new List<MenuItem>();

    // public bool IsDirty { get; private set; }

    // public IReadOnlyCollection<MenuItem> MenuItems
    // {
    // get
    // {
    // return this.menuItems.AsReadOnly();
    // }
    // }

    // public void AddMenuItem(MenuItem item)
    // {
    // this.menuItems.Add(item);
    // this.IsDirty = true;
    // }

    // public void RemoveMenuItem(MenuItem item)
    // {
    // if (this.menuItems.Remove(item))
    // {
    // this.IsDirty = true;
    // }
    // }
    // }
}