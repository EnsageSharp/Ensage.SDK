// <copyright file="PropertyBinding.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;
    using System.Reflection;

    using Ensage.SDK.Extensions;

    public class PropertyBinding
    {
        public PropertyBinding(PropertyInfo propertyInfo, object target)
        {
            this.PropertyInfo = propertyInfo;
            this.Reference = new WeakReference(target);

            this.Setter = propertyInfo.GetPropertySetter(target);
            this.Getter = propertyInfo.GetPropertyGetter(target);
        }

        public PropertyInfo PropertyInfo { get; }

        public WeakReference Reference { get; }

        private Func<object, object> Getter { get; }

        private Action<object, object> Setter { get; }

        public object GetValue()
        {
            return this.Getter(this.Reference.Target);
        }

        public T GetValue<T>()
        {
            return (T)this.GetValue();
        }

        public void SetValue(object value)
        {
            this.Setter(this.Reference.Target, value);
        }

        public override string ToString()
        {
            return $"{this.PropertyInfo?.DeclaringType?.Name}.{this.PropertyInfo?.Name} @ {this.Reference.Target}";
        }
    }
}