// <copyright file="ObjectValue.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;

    public class ObjectValue
    {
        public ObjectValue(object value)
        {
            this.Value = value;
            this.Type = value.GetType();
        }

        public ObjectValue()
        {
        }

        public Type Type { get; set; }

        public object Value { get; set; }
    }
}