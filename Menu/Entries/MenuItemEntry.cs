// <copyright file="MenuItemEntry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System.Reflection;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Views;
    using Ensage.SDK.Persistence;
    using Ensage.SDK.Renderer;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;

    public class MenuItemEntry : MenuBase
    {
        public MenuItemEntry(string name, IView view, IRenderer renderer, MenuConfig menuConfig, object instance, PropertyInfo propertyInfo)
            : this(name, null, view, renderer, menuConfig, instance, propertyInfo)
        {
        }

        public MenuItemEntry(string name, [CanBeNull] string textureKey, IView view, IRenderer renderer, MenuConfig menuConfig, object instance, PropertyInfo propertyInfo)
            : base(name, textureKey, view, renderer, menuConfig, instance, propertyInfo)
        {
            this.PropertyBinding = new PropertyBinding(propertyInfo, instance);
        }

        public PropertyBinding PropertyBinding { get; }

        public string Tooltip { get; set; }

        public object Value
        {
            get
            {
                return this.PropertyBinding.GetValue();
            }

            set
            {
                this.PropertyBinding.SetValue(value);
            }
        }

        public override void Draw()
        {
            this.View.Draw(this);
        }

        public override void OnClick(MouseButtons buttons, Vector2 clickPosition)
        {
            this.View.OnClick(this, buttons, clickPosition);
        }
    }
}