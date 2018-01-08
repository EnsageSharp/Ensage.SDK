// <copyright file="MenuEntry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Views;
    using Ensage.SDK.Renderer;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;

    public class MenuEntry : MenuBase
    {
        private readonly List<MenuBase> children = new List<MenuBase>();

        private bool isCollapsed = true;

        private bool isVisible = true;

        public MenuEntry(string name, View view, IRenderer renderer, MenuConfig menuConfig, object instance)
            : base(name, view, renderer, menuConfig, instance)
        {
        }

        public MenuEntry(string name, [CanBeNull] string textureKey, View view, IRenderer renderer, MenuConfig menuConfig, object instance)
            : base(name, textureKey, view, renderer, menuConfig, instance)
        {
        }

        public IReadOnlyCollection<MenuBase> Children
        {
            get
            {
                return this.children.AsReadOnly();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the menu entry is collapsed and if its children are visible.
        /// </summary>
        public bool IsCollapsed
        {
            get
            {
                return this.isCollapsed;
            }

            set
            {
                this.isCollapsed = value;
                foreach (var menuEntry in this.children.OfType<MenuEntry>())
                {
                    menuEntry.IsVisible = !this.isCollapsed;
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            private set
            {
                this.isVisible = value;
                foreach (var menuEntry in this.children.OfType<MenuEntry>())
                {
                    menuEntry.IsVisible = value;
                }
            }
        }

        /// <summary>
        ///     Gets or sets the total size of this menu entry, including the size of all children.
        /// </summary>
        public Vector2 TotalSize { get; set; }

        public void AddChild(MenuBase child)
        {
            this.children.Add(child);
        }

        public override void Reset()
        {
            foreach (var child in this.children)
            {
               child.Reset();
            }
        }

        public override void Draw()
        {
            this.View.Draw(this);
        }

        public bool IsInsideMenu(Vector2 position)
        {
            return this.Position.X <= position.X
                   && this.Position.Y <= position.Y
                   && position.X <= (this.Position.X + this.TotalSize.X)
                   && position.Y <= (this.Position.Y + this.TotalSize.Y);
        }

        public override void OnClick(MouseButtons buttons, Vector2 clickPosition)
        {
            this.View.OnClick(this, buttons, clickPosition);
        }

        public void RemoveChild(MenuBase child)
        {
            this.children.Remove(child);
        }
    }
}