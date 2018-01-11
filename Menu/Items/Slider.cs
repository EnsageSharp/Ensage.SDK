// <copyright file="Slider.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Menu.ValueBinding;

    using SharpDX;

    public class Slider<T> : ILoadable, ICloneable, ISlider<T>
    {
        private T value;

        public Slider()
        {
            var type = typeof(T);
            if (type != typeof(int) && type != typeof(float) && type != typeof(double) && type != typeof(Vector2))
            {
                throw new Exception($"Invalid type T given. Only {typeof(int)}, {typeof(float)}, {typeof(Vector2)} is allowed!");
            }
        }

        public Slider(T minValue, T maxValue)
            : this()
        {
            this.Value = minValue;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public Slider(T value, T minValue, T maxValue)
            : this()
        {
            this.Value = value;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public event EventHandler<ValueChangingEventArgs<T>> ValueChanging;

        public T MaxValue { get; set; }

        public T MinValue { get; set; }

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

                if (this.OnValueChanged(value))
                {
                    this.value = value;
                }
            }
        }

        object ISlider.MaxValue
        {
            get
            {
                return this.MaxValue;
            }
        }

        object ISlider.MinValue
        {
            get
            {
                return this.MinValue;
            }
        }

        object ISlider.Value
        {
            get
            {
                return this.Value;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Load(object data)
        {
            var slider = (ISlider<T>)data;

            if (!EqualityComparer<T>.Default.Equals(this.Value, slider.Value)
                && EqualityComparer<T>.Default.Equals(this.MinValue, slider.MinValue)
                && EqualityComparer<T>.Default.Equals(this.MaxValue, slider.MaxValue))
            {
                var sliderValues = (ISlider<T>)data;
                this.Value = sliderValues.Value;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        protected virtual bool OnValueChanged(T newValue)
        {
            var args = new ValueChangingEventArgs<T>(newValue, this.value);
            this.ValueChanging?.Invoke(this, args);
            return args.Process;
        }
    }
}