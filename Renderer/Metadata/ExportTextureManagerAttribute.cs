// <copyright file="ExportTextureManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportTextureManagerAttribute : ExportAttribute, IRendererMetadata
    {
        public ExportTextureManagerAttribute(RenderMode mode)
            : base(typeof(ITextureManager))
        {
            this.Mode = mode;
        }

        public RenderMode Mode { get; }
    }
}