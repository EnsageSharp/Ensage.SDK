// <copyright file="ExportTargetSelectorAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Service.Metadata;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportTargetSelectorAttribute : ObjectProviderAttribute, ITargetSelectorMetadata
    {
        public ExportTargetSelectorAttribute(string name, string version = null, string description = null)
            : base(typeof(ITargetSelector), name, version, description)
        {
        }
    }
}