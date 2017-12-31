// <copyright file="MenuItemEntry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;
    using System.Reflection;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Views;
    using Ensage.SDK.Persistence;
    using Ensage.SDK.Renderer;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public class MenuItemEntry : MenuBase
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MenuItemEntry(string name, IView view, IRenderer renderer, MenuConfig menuConfig, object instance, PropertyInfo propertyInfo)
            : this(name, null, view, renderer, menuConfig, instance, propertyInfo)
        {
            this.PropertyBinding = new PropertyBinding(propertyInfo, instance);
            this.AssignDefaultValue();
        }

        public void AssignDefaultValue()
        {
            if (this.Value is ICloneable clone)
            {
                this.DefaultValue = clone.Clone();
            }
            else if (this.Value.GetType().IsClass)
            {
                throw new Exception($"{this.Value.GetType().Name} doesn't implement {nameof(ICloneable)}");
            }
            else
            {
                this.DefaultValue = this.Value;
            }
        }

        public MenuItemEntry(string name, [CanBeNull] string textureKey, IView view, IRenderer renderer, MenuConfig menuConfig, object instance, PropertyInfo propertyInfo)
            : base(name, textureKey, view, renderer, menuConfig, instance, propertyInfo)
        {
            this.PropertyBinding = new PropertyBinding(propertyInfo, instance);
            this.AssignDefaultValue();
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

        public object DefaultValue { get; set; }

        public override void Reset()
        {
            Log.Debug($"Resetting {this.Value} to {this.DefaultValue}");
            if (this.DefaultValue is ICloneable clone)
            {
                this.Value = clone.Clone();
            }
            else
            {
                this.Value = this.DefaultValue;
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