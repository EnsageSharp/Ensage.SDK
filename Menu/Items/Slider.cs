// <copyright file="Slider.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;

    using Ensage.SDK.Menu.ValueBinding;

    public class Slider : ILoadable, ICloneable
    {
        private int value;

        public Slider()
        {
        }

        public Slider(int minValue, int maxValue)
        {
            this.Value = minValue;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public Slider(int value, int minValue, int maxValue)
        {
            this.Value = value;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public event EventHandler<ValueChangingEventArgs<int>> ValueChanging;

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.value == value)
                {
                    return;
                }

                if (this.OnValueChanged(value))
                {
                    this.value = value;
                }
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Load(object data)
        {
            var slider = (Slider)data;

            if (this.Value != slider.Value || this.MinValue != slider.MinValue || this.MaxValue != slider.MaxValue)
            {
                this.Value = slider.Value;
                this.MinValue = slider.MinValue;
                this.MaxValue = slider.MaxValue;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        protected virtual bool OnValueChanged(int newValue)
        {
            var args = new ValueChangingEventArgs<int>(newValue, this.value);
            this.ValueChanging?.Invoke(this, args);
            return args.Process;
        }
    }
}