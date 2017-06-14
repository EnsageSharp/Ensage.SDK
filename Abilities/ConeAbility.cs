// <copyright file="ConeAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Prediction;

    public abstract class ConeAbility : PredictionAbility
    {
        protected ConeAbility(Ability ability)
            : base(ability)
        {
        }

        public override PredictionSkillshotType PredictionSkillshotType { get; } = PredictionSkillshotType.SkillshotCone;
    }
}