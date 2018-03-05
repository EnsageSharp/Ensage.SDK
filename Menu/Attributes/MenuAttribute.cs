// <copyright file="MenuAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class MenuAttribute : Attribute
    {
        public MenuAttribute(string name = null)
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}