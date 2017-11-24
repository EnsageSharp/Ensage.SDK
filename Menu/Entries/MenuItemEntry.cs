// <copyright file="MenuItemEntry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System.Reflection;

    using Ensage.SDK.Persistence;
    using Ensage.SDK.Renderer;

    using SharpDX;

    public class MenuItemEntry : MenuBase
    {
        public MenuItemEntry(string name, IView view, IRenderer renderer, object instance, PropertyInfo propertyInfo)
            : base(name, view, renderer, instance)
        {
            this.PropertyInfo = propertyInfo;
            this.PropertyBinding = new PropertyBinding(propertyInfo, instance);
        }

        public PropertyInfo PropertyInfo { get; }

        public string Tooltip { get; set; }

        public PropertyBinding PropertyBinding { get; }

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