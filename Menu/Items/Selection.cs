// <copyright file="Selection.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Menu.ValueBinding;

    using Newtonsoft.Json;

    public class Selection<T> : ISelection<T>, ILoadable, ICloneable
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

        public event EventHandler<ValueChangingEventArgs<T>> ValueChanging;

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

        object[] ISelection.Values
        {
            get
            {
                return this.Values.OfType<object>().ToArray();
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int DecrementSelectedIndex()
        {
            var newIndex = this.SelectedIndex - 1;
            if (newIndex < 0)
            {
                newIndex = this.Values.Length - 1;
            }

            if (this.OnValueChanged(this.Values[newIndex]))
            {
                this.SelectedIndex = newIndex;
            }

            return this.SelectedIndex;
        }

        public int IncrementSelectedIndex()
        {
            var newIndex = this.SelectedIndex + 1;
            if (newIndex >= this.Values.Length)
            {
                newIndex = 0;
            }

            if (this.OnValueChanged(this.Values[newIndex]))
            {
                this.SelectedIndex = newIndex;
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

     

        protected virtual bool OnValueChanged(T newValue)
        {
            var args = new ValueChangingEventArgs<T>(newValue, this.Value);
            this.ValueChanging?.Invoke(this, args);
            return args.Process;
        }

        object ISelection.Value
        {
            get
            {
                return this.Value;
            }
        }
    }
}