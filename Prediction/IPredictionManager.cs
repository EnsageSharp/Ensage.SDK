// <copyright file="IPredictionManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    using Ensage.SDK.Prediction.Metadata;
    using Ensage.SDK.Service;

    public interface IPredictionManager : IServiceManager<IPrediction, IPredictionMetadata>, IPrediction
    {
        SDKConfig.PredictionConfig Config { get; }
    }
}