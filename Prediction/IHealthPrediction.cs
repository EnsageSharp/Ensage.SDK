// <copyright file="IHealthPrediction.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    public interface IHealthPrediction
    {
        float GetPredictedHealth(Unit unit, float untilTime);
    }
}