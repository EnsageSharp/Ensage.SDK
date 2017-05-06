// <copyright file="ImportTargetSelectorManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportTargetSelectorManagerAttribute : ImportAttribute
    {
        public ImportTargetSelectorManagerAttribute()
            : base(typeof(ITargetSelectorManager))
        {
        }
    }
}