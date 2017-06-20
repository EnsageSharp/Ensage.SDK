// <copyright file="MenuItem.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System.ComponentModel;
    using System.Windows.Input;

    using Ensage.Common.Menu;
    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class MenuItem<TType> : INotifyPropertyChanged
    {
        public MenuItem(string displayName, string name, TType value)
        {
            this.Item = new MenuItem(name, displayName);
            this.Item.SetValue(value);
            this.Item.ValueChanged += this.ItemOnValueChanged;
        }

        public MenuItem(string displayName, string name)
        {
            this.Item = new MenuItem(name, displayName);
            this.Item.ValueChanged += this.ItemOnValueChanged;
        }

        public MenuItem(MenuItem item)
        {
            this.Item = item;
            this.Item.ValueChanged += this.ItemOnValueChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MenuItem Item { get; }

        public TType Value
        {
            get
            {
                return this.Item.GetValue<TType>();
            }

            set
            {
                this.Item.SetValue(value);
            }
        }

        public static implicit operator bool(MenuItem<TType> item)
        {
            if (item?.Item == null)
            {
                return false;
            }

            return item.Item.IsActive();
        }

        public static implicit operator string(MenuItem<TType> item)
        {
            return item.Item.GetValue<StringList>().SelectedValue;
        }

        public static implicit operator Key(MenuItem<TType> item)
        {
            return KeyInterop.KeyFromVirtualKey((int)item.Item.GetValue<KeyBind>().Key);
        }

        public static implicit operator int(MenuItem<TType> item)
        {
            return item.Item.GetValue<Slider>().Value;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged()
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        }

        private void ItemOnValueChanged(object sender, OnValueChangeEventArgs args)
        {
            UpdateManager.BeginInvoke(this.OnPropertyChanged);
        }
    }
}