// <copyright file="MenuItemEntry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System.Reflection;

    using Ensage.SDK.Menu.Views;
    using Ensage.SDK.Persistence;
    using Ensage.SDK.Renderer;

    using SharpDX;

    public class MenuItemEntry : MenuBase
    {
        public MenuItemEntry(string name, IView view, IRenderer renderer, StyleRepository styleRepository, object instance, PropertyInfo propertyInfo)
            : base(name, view, renderer, styleRepository, instance, propertyInfo)
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

        public override void OnClick(Vector2 clickPosition)
        {
            this.View.OnClick(this, clickPosition);
        }
    }
}