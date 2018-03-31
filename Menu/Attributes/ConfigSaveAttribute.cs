// <copyright file="MenuSaveOnlyAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigSaveAttribute : Attribute
    {
        public ConfigSaveAttribute()
        {
        }
    }
}