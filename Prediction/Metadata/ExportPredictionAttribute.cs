// <copyright file="ExportPredictionAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Prediction;
    using Ensage.SDK.Service.Metadata;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportPredictionAttribute : ObjectProviderAttribute, IPredictionMetadata
    {
        public ExportPredictionAttribute(string name, string version = null, string description = null)
            : base(typeof(IPrediction), name, version, description)
        {
        }
    }
}