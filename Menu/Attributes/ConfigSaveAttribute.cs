// <copyright file="MenuSaveOnlyAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ConfigSaveAttribute : Attribute
    {
        public ConfigSaveAttribute(bool save = true)
        {
            this.Save = save;
        }

        public bool Save { get; }
    }
}