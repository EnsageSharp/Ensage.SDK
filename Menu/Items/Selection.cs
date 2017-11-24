// <copyright file="Selection.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    public class Selection<T>
    {
        public Selection(int selectedIndex, params T[] value)
        {
            this.Values = value;
            this.SelectedIndex = selectedIndex;
        }

        public int SelectedIndex { get; set; }

        public T Value
        {
            get
            {
                return this.Values[this.SelectedIndex];
            }

            set
            {
                this.SelectedIndex = Array.IndexOf(this.Values, value);
            }
        }

        public T[] Values { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}