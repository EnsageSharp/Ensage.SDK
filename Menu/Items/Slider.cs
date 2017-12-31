// <copyright file="Slider.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    public class Slider : ILoadable, ICloneable
    {
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

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public int Value { get; set; }

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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}