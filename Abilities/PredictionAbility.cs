// <copyright file="PredictionAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Prediction;
    using Ensage.SDK.Prediction.Collision;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    public abstract class PredictionAbility : RangedAbility
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected PredictionAbility(Ability ability, IPrediction prediction = null)
            : base(ability)
        {
            this.Prediction = prediction ?? IoC.Get<IPrediction>();
        }

        public virtual CollisionTypes CollisionTypes { get; } = CollisionTypes.None;

        public virtual float EndRadius
        {
            get
            {
                return this.Radius;
            }
        }

        public virtual bool HasAreaOfEffect
        {
            get
            {
                return this.Ability.AbilityBehavior.HasFlag(AbilityBehavior.AreaOfEffect);
            }
        }

        public abstract PredictionSkillshotType PredictionSkillshotType { get; }

        public virtual float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public virtual float Range
        {
            get
            {
                return this.CastRange;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        protected IPrediction Prediction { get; }

        public virtual PredictionInput GetPredictionInput(params Unit[] targets)
        {
            var input = new PredictionInput
            {
                Owner = this.Owner,
                AreaOfEffect = this.HasAreaOfEffect,
                CollisionTypes = this.CollisionTypes,
                Delay = this.CastPoint + this.ActivationDelay,
                Speed = this.Speed,
                Range = this.CastRange,
                Radius = this.Radius,
                PredictionSkillshotType = this.PredictionSkillshotType
            };

            if (this.HasAreaOfEffect)
            {
                input.AreaOfEffectTargets = targets;
            }

            return input.WithTarget(targets.First());
        }

        public virtual PredictionOutput GetPredictionOutput(PredictionInput input)
        {
            return this.Prediction.GetPrediction(input);
        }

        public bool UseAbility(Unit target, HitChance minimChance)
        {
            if (!this.CanBeCasted)
            {
                return false;
            }

            var predictionInput = this.GetPredictionInput(target);
            var output = this.GetPredictionOutput(predictionInput);
            if (output.HitChance == HitChance.OutOfRange || output.HitChance == HitChance.Impossible)
            {
                return false;
            }

            if (predictionInput.CollisionTypes != CollisionTypes.None && output.HitChance == HitChance.Collision)
            {
                return false;
            }

            if (output.HitChance < minimChance)
            {
                return false;
            }

            return this.UseAbility(output.CastPosition);
        }

        public override bool UseAbility(Unit target)
        {
            return this.UseAbility(target, HitChance.Medium); // TODO: get prediction config hitchance value
        }

        // TODO: add other UseAbility overload without parameter etc to automatically get best position for clock rockets, magnus ult etc?
    }
}