// <copyright file="ImportOrbwalkerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportOrbwalkerAttribute : ImportAttribute
    {
        public ImportOrbwalkerAttribute()
            : base(typeof(IOrbwalker))
        {
        }
    }
}