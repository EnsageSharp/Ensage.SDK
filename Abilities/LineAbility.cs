// <copyright file="LineAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Prediction;

    public abstract class LineAbility : PredictionAbility
    {
        protected LineAbility(Ability ability)
            : base(ability)
        {
        }

        public override PredictionSkillshotType PredictionSkillshotType { get; } = PredictionSkillshotType.SkillshotLine;

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < (this.Range + this.Radius));
        }
    }
}