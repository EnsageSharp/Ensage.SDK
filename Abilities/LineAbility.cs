// <copyright file="LineAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Prediction;

    public abstract class LineAbility : PredictionAbility
    {
        protected LineAbility(Ability abiltity)
            : base(abiltity)
        {
        }

        public override PredictionSkillshotType PredictionSkillshotType { get; } = PredictionSkillshotType.SkillshotLine;
    }
}