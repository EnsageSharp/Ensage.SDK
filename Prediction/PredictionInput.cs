// <copyright file="PredictionInput.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    using System.Collections.Generic;

    using Ensage.SDK.Prediction.Collision;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class PredictionInput
    {
        public PredictionInput()
        {
        }

        public PredictionInput(
            Unit owner,
            Unit target,
            float delay,
            float speed,
            float range,
            float radius,
            PredictionSkillshotType type,
            bool areaOfEffect = false,
            bool areaOfEffectHitMainTarget = true,
            IReadOnlyList<Unit> aoeTargets = null)
        {
            this.Owner = owner;
            this.Target = target;
            this.Delay = delay;
            this.Speed = speed;
            this.Range = range;
            this.Radius = radius;

            this.PredictionSkillshotType = type;
            this.AreaOfEffect = areaOfEffect;
            this.AreaOfEffectHitMainTarget = areaOfEffectHitMainTarget;
            this.AreaOfEffectTargets = aoeTargets ?? new Unit[0];
        }

        public PredictionInput(Unit owner, Unit target, float delay, float speed, float range, float radius)
        {
            this.Owner = owner;
            this.Target = target;
            this.Delay = delay;
            this.Speed = speed;
            this.Range = range;
            this.Radius = radius;

            this.PredictionSkillshotType = PredictionSkillshotType.SkillshotLine;
        }

        public bool AreaOfEffect { get; set; } = false;

        public bool AreaOfEffectHitMainTarget { get; set; } = true;

        public IReadOnlyList<Unit> AreaOfEffectTargets { get; set; }

        public CollisionTypes CollisionTypes { get; set; } = CollisionTypes.None;

        public float Delay { get; set; } = 0f;

        public Unit Owner { get; set; }

        public PredictionSkillshotType PredictionSkillshotType { get; set; } = PredictionSkillshotType.SkillshotCircle;

        public float Radius { get; set; } = 0f;

        public float Range { get; set; } = float.MaxValue;

        public float Speed { get; set; } = float.MaxValue;

        public Unit Target { get; private set; }

        public PredictionInput WithTarget([NotNull] Unit target)
        {
            var input = (PredictionInput)this.MemberwiseClone();
            input.Target = target;

            return input;
        }
    }
}