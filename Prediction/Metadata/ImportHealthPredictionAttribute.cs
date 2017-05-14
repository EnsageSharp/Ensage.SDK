// <copyright file="ImportHealthPredictionAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportHealthPredictionAttribute : ImportAttribute
    {
        public ImportHealthPredictionAttribute()
            : base(typeof(IHealthPrediction))
        {
        }
    }
}