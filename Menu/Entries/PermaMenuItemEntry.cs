// <copyright file="PermaMenuItemEntry.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Views;
    using Ensage.SDK.Renderer;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;

    public class PermaMenuItemEntry : MenuItemEntry
    {
        public PermaMenuItemEntry(string name, View view, IRenderer renderer, MenuConfig menuConfig, ValueBinding valueBinding)
            : base(name, view, renderer, menuConfig, valueBinding)
        {
        }

        public PermaMenuItemEntry(string name, [CanBeNull] string textureKey, View view, IRenderer renderer, MenuConfig menuConfig, ValueBinding valueBinding)
            : base(name, textureKey, view, renderer, menuConfig, valueBinding)
        {
        }

        public Vector2 PermaRenderSize { get; set; }

        public Vector2 PermaSize
        {
            get
            {
                return this.View.GetPermaSize(this);
            }
        }

        public Vector2 PermaTextSize { get; set; }

        public string RootMenuName { get; set; } = string.Empty;

        public Vector2 PermaDraw(Vector2 position)
        {
            return this.View.PermaDraw(this, position);
        }
    }
}