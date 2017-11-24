// <copyright file="ItemAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class ItemAttribute : Attribute
    {
        public ItemAttribute(string name = null)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}