// <copyright file="ValueType.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.ValueBinding
{
    using System;
    using System.Collections.Generic;

    public class ValueType<T>
    {
        private T value;

        public ValueType()
        {
        }

        public ValueType(T value)
        {
            this.value = value;
        }

        public event EventHandler<ValueChangingEventArgs<T>> Changed;

        public T Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    return;
                }

                var args = new ValueChangingEventArgs<T>(value, this.value);

                this.Changed?.Invoke(this, args);

                if (!args.Process)
                {
                    return;
                }

                this.value = value;
            }
        }

        public static implicit operator T(ValueType<T> valueType)
        {
            return valueType.Value;
        }
    }


}