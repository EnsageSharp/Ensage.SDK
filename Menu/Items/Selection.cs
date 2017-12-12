// <copyright file="Selection.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public class Selection<T> : ISelection<T>, ILoadable
    {
        public Selection()
        {
        }

        public Selection(params T[] value)
        {
            this.Values = value;
            this.SelectedIndex = Array.IndexOf(this.Values, this.Values.First());
        }

        public Selection(IEnumerable<T> values)
            : this(values.ToArray())
        {
        }

        public Selection(int selectedIndex, params T[] value)
        {
            this.Values = value;
            this.SelectedIndex = selectedIndex;
        }

        public int SelectedIndex { get; set; }

        [JsonIgnore]
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

        public T this[int index]
        {
            get
            {
                return this.Values[index];
            }

            set
            {
                this.Values[index] = value;
            }
        }

        /// <summary>
        ///     Converts to <see cref="Value" />
        /// </summary>
        /// <param name="selection"></param>
        public static implicit operator T(Selection<T> selection)
        {
            return selection.Value;
        }

        /// <summary>
        ///     Converts to <see cref="SelectedIndex" />
        /// </summary>
        /// <param name="selection"></param>
        public static implicit operator int(Selection<T> selection)
        {
            return selection.SelectedIndex;
        }

        public int DecrementSelectedIndex()
        {
            this.SelectedIndex--;
            if (this.SelectedIndex < 0)
            {
                this.SelectedIndex = this.Values.Length - 1;
            }

            return this.SelectedIndex;
        }

        public int IncrementSelectedIndex()
        {
            this.SelectedIndex++;
            if (this.SelectedIndex >= this.Values.Length)
            {
                this.SelectedIndex = 0;
            }

            return this.SelectedIndex;
        }

        public bool Load(object data)
        {
            var selection = (Selection<T>)data;

            if (this.Values.SequenceEqual(selection.Values))
            {
                this.Values = selection.Values;
                this.Value = selection.Value;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}