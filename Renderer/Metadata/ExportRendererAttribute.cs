// <copyright file="ExportRendererAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportRendererAttribute : ExportAttribute, IRendererMetadata
    {
        public ExportRendererAttribute(RenderMode mode)
            : base(typeof(IRenderer))
        {
            this.Mode = mode;
        }

        public RenderMode Mode { get; }
    }
}