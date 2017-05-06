// <copyright file="ExportTargetSelectorManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportTargetSelectorManagerAttribute : ExportAttribute, ITargetSelectorManagerMetadata
    {
        public ExportTargetSelectorManagerAttribute()
            : base(typeof(ITargetSelectorManager))
        {
        }
    }
}