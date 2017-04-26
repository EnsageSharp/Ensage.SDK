// <copyright file="ExportOrbwalkerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Service.Metadata;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportOrbwalkerAttribute : ObjectProviderAttribute, IOrbwalkerMetadata
    {
        public ExportOrbwalkerAttribute(string name, string version = null, string description = null)
            : base(typeof(IOrbwalker), name, version, description)
        {
        }
    }
}