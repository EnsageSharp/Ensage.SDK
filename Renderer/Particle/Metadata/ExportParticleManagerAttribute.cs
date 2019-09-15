// <copyright file="ExportParticleManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Particle.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportParticleManagerAttribute : ExportAttribute, IParticleManagerMetadata
    {
        public ExportParticleManagerAttribute()
            : base(typeof(IParticleManager))
        {
        }
    }
}