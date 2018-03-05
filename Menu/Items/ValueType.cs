// <copyright file="ValueType.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.ValueBinding
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Menu.Items;

    using Newtonsoft.Json;

    public interface IValueType
    {
        [JsonIgnore]
        Type Type { get; }

        object Value { get; set; }
    }

    public interface IValueType<out T> : IValueType
    {
        new T Value { get; }
    }

    public class ValueType<T> : IValueType<T>, ILoadable, ICloneable, IEquatable<ValueType<T>>
    {
        private T value;

        public ValueType()
        {
            this.Type = typeof(T);
        }

        public ValueType(T value)
        {
            this.Type = typeof(T);
            this.value = value;
        }

        public event EventHandler<ValueChangingEventArgs<T>> Changed;

        [JsonIgnore]
        public Type Type { get; }

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

        object IValueType.Value
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = (T)value;
            }
        }

        public static bool operator ==(ValueType<T> left, ValueType<T> right)
        {
            return Equals(left, right);
        }

        public static implicit operator T(ValueType<T> valueType)
        {
            return valueType.Value;
        }

        public static bool operator !=(ValueType<T> left, ValueType<T> right)
        {
            return !Equals(left, right);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Equals(ValueType<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return EqualityComparer<T>.Default.Equals(this.value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ValueType<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(this.value);
        }

        public bool Load(object data)
        {
            var other = (ValueType<T>)data;
            if (!EqualityComparer<T>.Default.Equals(this.Value, other.Value))
            {
                this.Value = other.Value;
                return true;
            }

            return false;
        }
    }
}