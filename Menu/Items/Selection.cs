// <copyright file="Selection.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    public interface ISelection<out T>
    {
        int SelectedIndex { get; }

        T Value { get; }

        T[] Values { get; }

        int IncrementSelectedIndex();

        int DecrementSelectedIndex();
    }

    public class Selection<T> : ISelection<T>, ILoadable
    {
        public Selection()
        {
        }

        public Selection(params T[] value)
        {
            Values = value;
            SelectedIndex = Array.IndexOf(Values, Values.First());
        }

        public Selection(IEnumerable<T> values)
            : this(values.ToArray())
        {
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

        public int IncrementSelectedIndex()
        {
            SelectedIndex++;
            if (SelectedIndex >= Values.Length)
            {
                SelectedIndex = 0;
            }

            return SelectedIndex;
        }

        public int DecrementSelectedIndex()
        {
            SelectedIndex--;
            if (SelectedIndex < 0)
            {
                SelectedIndex = Values.Length - 1;
            }

            return SelectedIndex;
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

        public bool Load(object data)
        {
            var selection = (Selection<T>)data;

            if (!Values.SequenceEqual(selection.Values))
            {
                Values = selection.Values;
                Value = selection.Value;
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