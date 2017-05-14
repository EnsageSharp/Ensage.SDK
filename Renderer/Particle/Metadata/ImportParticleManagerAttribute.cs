// <copyright file="ImportParticleManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Particle.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportParticleManagerAttribute : ImportAttribute
    {
        public ImportParticleManagerAttribute()
            : base(typeof(IParticleManager))
        {
        }
    }
}