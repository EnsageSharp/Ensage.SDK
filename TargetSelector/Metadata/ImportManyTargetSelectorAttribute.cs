// <copyright file="ImportManyTargetSelectorAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportManyTargetSelectorAttribute : ImportManyAttribute
    {
        public ImportManyTargetSelectorAttribute()
            : base(typeof(ITargetSelector))
        {
            this.AllowRecomposition = true;
            this.RequiredCreationPolicy = CreationPolicy.Any;
        }
    }
}