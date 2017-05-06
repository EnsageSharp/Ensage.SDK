// <copyright file="ImportManyOrbwalkerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportManyOrbwalkerAttribute : ImportManyAttribute
    {
        public ImportManyOrbwalkerAttribute()
            : base(typeof(IOrbwalker))
        {
            this.AllowRecomposition = true;
            this.RequiredCreationPolicy = CreationPolicy.Any;
        }
    }
}