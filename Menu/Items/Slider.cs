// <copyright file="Slider.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;

    public class Slider : ILoadable
    {
        public Slider()
        {
        }

        public Slider(int minValue, int maxValue)
        {
            Value = minValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public Slider(int value, int minValue, int maxValue)
        {
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public int Value { get; set; }

        public bool Load(object data)
        {
            var slider = (Slider)data;

            if (Value != slider.Value || MinValue != slider.MinValue || MaxValue != slider.MaxValue)
            {
                Value = slider.Value;
                MinValue = slider.MinValue;
                MaxValue = slider.MaxValue;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}