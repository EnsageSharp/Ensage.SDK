// <copyright file="MenuItemEntry.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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

    public abstract class ValueBinding
    {
        public abstract string Name { get; }

        public abstract object Value { get; set; }

        public abstract Type ValueType { get; }

        public abstract T GetValue<T>();

        public abstract void SetValue<T>(T value);

        [CanBeNull]
        public virtual T GetCustomAttribute<T>()
            where T : Attribute
        {
            return default(T);
        }
    }

    public class ValuePropertyBinding : ValueBinding
    {
        private readonly string name;

        private readonly Type valueType;

        public ValuePropertyBinding(object instance, PropertyInfo propertyInfo)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            this.PropertyBinding = new PropertyBinding(propertyInfo, instance);
            this.valueType = this.PropertyBinding.PropertyInfo.PropertyType;
            this.name = propertyInfo.Name;
        }

        public override string Name
        {
            get
            {
                return this.name;
            }
        }

        public override T GetCustomAttribute<T>() 
        {
            return this.PropertyBinding.PropertyInfo.GetCustomAttribute<T>();
        }

        public PropertyBinding PropertyBinding { get; }

        public override object Value
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

        public override Type ValueType
        {
            get
            {
                return this.valueType;
            }
        }

        public override T GetValue<T>()
        {
            return (T)this.PropertyBinding.GetValue();
        }

        public override void SetValue<T>(T value)
        {
            this.PropertyBinding.SetValue(value);
        }
    }

    public class ValueDictionaryBinding : ValueBinding
    {
        private readonly Type valueType;

        public ValueDictionaryBinding(string key, Dictionary<string, object> dictionary)
        {
            this.Name = key;
            this.Dictionary = dictionary;
            this.valueType = this.Dictionary[this.Name].GetType();
        }

        public Dictionary<string, object> Dictionary { get; }

        public override string Name { get; }

        public override object Value
        {
            get
            {
                return this.Dictionary[this.Name];
            }

            set
            {
                this.Dictionary[this.Name] = value;
            }
        }

        public override Type ValueType
        {
            get
            {
                return this.valueType;
            }
        }

        public override T GetValue<T>()
        {
            return (T)this.Dictionary[this.Name];
        }

        public override void SetValue<T>(T value)
        {
            this.Dictionary[this.Name] = value;
        }
    }

    public class MenuItemEntry : MenuBase
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MenuItemEntry(string name, IView view, IRenderer renderer, MenuConfig menuConfig, ValueBinding valueBinding)
            : this(name, null, view, renderer, menuConfig, valueBinding)
        {
            this.AssignDefaultValue();
        }

        public MenuItemEntry(string name, [CanBeNull] string textureKey, IView view, IRenderer renderer, MenuConfig menuConfig, ValueBinding valueBinding)
            : base(name, textureKey, view, renderer, menuConfig, valueBinding)
        {
            this.ValueBinding = valueBinding;
            this.AssignDefaultValue();
        }

        public object DefaultValue { get; set; }

        public string Tooltip { get; set; }

        public object Value
        {
            get
            {
                return this.ValueBinding.Value;
            }

            set
            {
                this.ValueBinding.Value = value;
            }
        }

        public ValueBinding ValueBinding { get; }

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

        public override void Draw()
        {
            this.View.Draw(this);
        }

        public override void OnClick(MouseButtons buttons, Vector2 clickPosition)
        {
            this.View.OnClick(this, buttons, clickPosition);
        }

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
    }
}