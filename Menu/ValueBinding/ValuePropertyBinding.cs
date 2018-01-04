// <copyright file="ValuePropertyBinding.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;
    using System.Reflection;

    using Ensage.SDK.Persistence;

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

        public override T GetCustomAttribute<T>()
        {
            return this.PropertyBinding.PropertyInfo.GetCustomAttribute<T>();
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
}