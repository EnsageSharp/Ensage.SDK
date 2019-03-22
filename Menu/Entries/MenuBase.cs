// <copyright file="MenuBase.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Reflection;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Views;
    using Ensage.SDK.Renderer;

    using Newtonsoft.Json.Linq;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;

    public abstract class MenuBase
    {
        protected MenuBase(string name, View view, IRenderer renderer, MenuConfig menuConfig, object instance)
            : this(name, null, view, renderer, menuConfig, instance)
        {
        }

        protected MenuBase(string name, [CanBeNull] string textureKey, View view, IRenderer renderer, MenuConfig menuConfig, object instance)
        {
            this.MenuConfig = menuConfig;
            this.Name = name;
            this.TextureKey = textureKey;
            this.View = view;
            this.Renderer = renderer;
            this.DataContext = instance;
        }

        public JToken LoadToken { get; set; }

        public object DataContext { get; }

        public bool IsHovered { get; internal set; }

        public MenuConfig MenuConfig { get; }

        public string Name { get; }

        public string TextureKey { get; set; }

        public Vector2 Position { get; set; }

        public IRenderer Renderer { get; }

        /// <summary>
        ///     Gets or sets the size at which this item is rendered. The width will be aligned to the other menu items of the
        ///     parent.
        /// </summary>
        public Vector2 RenderSize { get; set; }

        /// <summary>
        ///     Gets the actual size of the menu item.
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return this.View.GetSize(this);
            }
        }

        public Vector2 TextSize { get; set; }

        public abstract void Reset();

        public View View { get; }

        public abstract void Draw();

        public bool IsInside(Vector2 screenPosition)
        {
            return this.Position.X <= screenPosition.X
                   && this.Position.Y <= screenPosition.Y
                   && screenPosition.X <= (this.Position.X + this.RenderSize.X)
                   && screenPosition.Y <= (this.Position.Y + this.RenderSize.Y);
        }

        public abstract void OnClick(MouseButtons buttons, Vector2 clickPosition);

        public override string ToString()
        {
            return this.Name;
        }
    }
}