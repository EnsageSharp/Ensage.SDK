// <copyright file="ValueBinding.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Entries
{
    using System;

    using PlaySharp.Toolkit.Helper.Annotations;

    public abstract class ValueBinding
    {
        public abstract string Name { get; }

        public abstract object Value { get; set; }

        public abstract Type ValueType { get; }

        [CanBeNull]
        public virtual T GetCustomAttribute<T>()
            where T : Attribute
        {
            return default(T);
        }

        public abstract T GetValue<T>();

        public abstract void SetValue<T>(T value);
    }
}