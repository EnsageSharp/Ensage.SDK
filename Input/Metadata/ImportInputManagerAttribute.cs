// <copyright file="ImportInputManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportInputManagerAttribute : ImportAttribute
    {
        public ImportInputManagerAttribute()
            : base(typeof(IInputManager))
        {
        }
    }
}