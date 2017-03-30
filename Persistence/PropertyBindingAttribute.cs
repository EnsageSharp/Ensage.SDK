// <copyright file="PropertyBindingAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PropertyBindingAttribute : Attribute
    {
        public PropertyBindingAttribute(string name = null)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}