// <copyright file="ExportTargetSelectorAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.ComponentModel.Composition;
    using System.Security;

    using Ensage.SDK.Attributes;
    using Ensage.SDK.TargetSelector;

    [MetadataAttribute]
    [SecuritySafeCritical]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportOrbwalkerAttribute : ObjectProviderAttribute, ITargetSelectorMetadata
    {
        public ExportOrbwalkerAttribute(string name, string version = null, string description = null)
            : base(typeof(IOrbwalker), name, version, description)
        {
        }
    }
}