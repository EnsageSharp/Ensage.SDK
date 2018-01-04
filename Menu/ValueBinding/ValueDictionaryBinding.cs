// <copyright file="ValueDictionaryBinding.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;
    using System.Collections.Generic;

    public class ValueDictionaryBinding : ValueBinding
    {
        private readonly Type valueType;

        public ValueDictionaryBinding(string key, Dictionary<string, object> dictionary)
        {
            this.Name = key;
            this.Dictionary = dictionary;
            this.valueType = this.Dictionary[this.Name].GetType();
        }

        public Dictionary<string, object> Dictionary { get; }

        public override string Name { get; }

        public override object Value
        {
            get
            {
                return this.Dictionary[this.Name];
            }

            set
            {
                this.Dictionary[this.Name] = value;
            }
        }

        public override Type ValueType
        {
            get
            {
                return this.valueType;
            }
        }

        public override T GetValue<T>()
        {
            return (T)this.Dictionary[this.Name];
        }

        public override void SetValue<T>(T value)
        {
            this.Dictionary[this.Name] = value;
        }
    }
}