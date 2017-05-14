// <copyright file="ExportOrbwalkingModeAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportOrbwalkingModeAttribute : ExportAttribute, IOrbwalkingModeMetadata
    {
        public ExportOrbwalkingModeAttribute()
            : base(typeof(IOrbwalkingMode))
        {
        }
    }
}