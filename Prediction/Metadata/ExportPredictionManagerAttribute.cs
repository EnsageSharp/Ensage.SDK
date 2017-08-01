// <copyright file="ExportPredictionManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportPredictionManagerAttribute : ExportAttribute, IPredictionManagerMetadata
    {
        public ExportPredictionManagerAttribute()
            : base(typeof(IPredictionManager))
        {
        }
    }
}