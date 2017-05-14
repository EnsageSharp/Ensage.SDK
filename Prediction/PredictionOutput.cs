// <copyright file="PredictionOutput.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    using System.Collections.Generic;

    using Ensage.SDK.Prediction.Collision;

    using SharpDX;

    public class PredictionOutput
    {
        public IReadOnlyList<PredictionOutput> AoeTargetsHit { get; set; }

        public float ArrivalTime { get; set; }

        public Vector3 CastPosition { get; set; }

        public CollisionResult CollisionResult { get; set; }

        public HitChance HitChance { get; set; }

        public Unit Unit { get; set; }

        public Vector3 UnitPosition { get; set; }
    }
}