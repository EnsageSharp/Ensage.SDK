// <copyright file="Selection.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.Linq;

    using Newtonsoft.Json;

    public class Selection<T>
    {
        public Selection()
        {
            
        }

        public Selection(params T[] value)
        {
            Values = value;
            SelectedIndex = Array.IndexOf(this.Values, Values.First());
        }

        public Selection(int selectedIndex, params T[] value)
        {
            Values = value;
            SelectedIndex = selectedIndex;
        }

        public int SelectedIndex { get; set; }

        [JsonIgnore]
        public T Value
        {
            get
            {
                return Values[SelectedIndex];
            }

            set
            {
                SelectedIndex = Array.IndexOf(Values, value);
            }
        }

        public T[] Values { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}