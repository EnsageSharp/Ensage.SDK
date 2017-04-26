// <copyright file="ExportInputManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportInputManagerAttribute : ExportAttribute, IInputManagerMetadata
    {
        public ExportInputManagerAttribute()
            : base(typeof(IInputManager))
        {
        }
    }
}