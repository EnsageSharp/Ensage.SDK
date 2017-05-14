// <copyright file="IPrediction.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    public interface IPrediction
    {
        PredictionOutput GetPrediction(PredictionInput input);
    }
}