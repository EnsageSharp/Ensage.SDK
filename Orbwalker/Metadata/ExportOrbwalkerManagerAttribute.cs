// <copyright file="ExportOrbwalkerManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    internal class ExportOrbwalkerManagerAttribute : ExportAttribute, IOrbwalkerManagerMetadata
    {
        public ExportOrbwalkerManagerAttribute()
            : base(typeof(IOrbwalkerManager))
        {
        }
    }
}