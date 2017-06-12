﻿// <copyright file="Prediction.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Prediction.Collision;
    using Ensage.SDK.Prediction.Metadata;

    using SharpDX;

    [ExportPrediction("SDK")]
    public class Prediction : IPrediction
    {
        [ImportingConstructor]
        public Prediction()
        {
            this.Pathfinder = new NavMeshPathfinding();
        }

        private NavMeshPathfinding Pathfinder { get; }

        public PredictionOutput GetPrediction(PredictionInput input)
        {
            var result = this.GetSimplePrediction(input);

            // Handle AreaOfEffect
            if (input.AreaOfEffect)
            {
                result = this.GetAreaOfEffectPrediction(input, result);
            }

            // check range
            if (input.Range != float.MaxValue)
            {
                if (!this.IsInRange(input, result.CastPosition, false))
                {
                    result.HitChance = HitChance.OutOfRange;
                }
                else if (!this.IsInRange(input, result.UnitPosition, true))
                {
                    result.HitChance = HitChance.OutOfRange;
                }
            }

            // check collision
            if (input.CollisionTypes != CollisionTypes.None)
            {
                var scanRange = input.Owner.Distance2D(result.CastPosition);
                var movingObjects = new List<Unit>();
                var collisionObjects = new List<CollisionObject>();

                if (input.CollisionTypes.HasFlag(CollisionTypes.AllyCreeps))
                {
                    movingObjects.AddRange(
                        EntityManager<Creep>.Entities.Where(
                            unit => unit.IsAlly(input.Owner) && unit.IsValidTarget(float.MaxValue, false) && input.Owner.IsInRange(unit, scanRange)));
                }

                if (input.CollisionTypes.HasFlag(CollisionTypes.EnemyCreeps))
                {
                    movingObjects.AddRange(
                        EntityManager<Creep>.Entities.Where(
                            unit => unit.IsEnemy(input.Owner) && unit.IsValidTarget(float.MaxValue, false) && input.Owner.IsInRange(unit, scanRange)));
                }

                if (input.CollisionTypes.HasFlag(CollisionTypes.AllyHeroes))
                {
                    movingObjects.AddRange(
                        EntityManager<Hero>.Entities.Where(
                            unit => unit.IsAlly(input.Owner) &&
                                    unit.IsValidTarget(float.MaxValue, false) &&
                                    input.Owner.IsInRange(unit, scanRange) &&
                                    unit != input.Owner));
                }

                if (input.CollisionTypes.HasFlag(CollisionTypes.EnemyHeroes))
                {
                    movingObjects.AddRange(
                        EntityManager<Hero>.Entities.Where(
                            unit => unit.IsEnemy(input.Owner) &&
                                    unit.IsValidTarget(float.MaxValue, false) &&
                                    input.Owner.IsInRange(unit, scanRange) &&
                                    unit != input.Target));
                }

                // add units
                foreach (var unit in movingObjects)
                {
                    var predictedPos = this.GetSimplePrediction(input.WithTarget(unit));
                    collisionObjects.Add(new CollisionObject(unit, predictedPos.UnitPosition, unit.HullRadius + 10f));
                    collisionObjects.Add(new CollisionObject(unit, unit.NetworkPosition, unit.HullRadius)); // optional
                }

                // add trees and buildings, use NavMeshCellFlags for less lag?
                if (input.CollisionTypes.HasFlag(CollisionTypes.Trees))
                {
                    foreach (var tree in EntityManager<Tree>.Entities.Where(unit => input.Owner.IsInRange(unit, scanRange)))
                    {
                        collisionObjects.Add(new CollisionObject(tree, tree.NetworkPosition, 75f));
                    }
                }

                var collisionResult = Collision.Collision.GetCollision(input.Owner.NetworkPosition.ToVector2(), result.CastPosition.ToVector2(), input.Radius, collisionObjects);
                if (collisionResult.Collides)
                {
                    result.HitChance = HitChance.Collision;
                }

                result.CollisionResult = collisionResult;
            }

            return result;
        }

        private static PredictionOutput PredictionOutput(Unit target, Vector3 position, HitChance hitChance)
        {
            return new PredictionOutput
                   {
                       Unit = target,
                       CastPosition = position,
                       UnitPosition = position,
                       HitChance = hitChance
                   };
        }

        private Vector3 ExtendUntilWall(Vector3 start, Vector3 direction, float distance)
        {
            var step = this.Pathfinder.CellSize / 2f;
            var testPoint = start;
            var sign = distance > 0f ? 1f : -1f;

            distance = Math.Abs(distance);

            while (this.Pathfinder.GetCellFlags(testPoint).HasFlag(NavMeshCellFlags.Walkable) && distance > 0f)
            {
                if (step > distance)
                {
                    step = distance;
                }

                testPoint = testPoint + (sign * direction * step);
                distance -= step;
            }

            return testPoint;
        }

        private PredictionOutput GetAreaOfEffectPrediction(PredictionInput input, PredictionOutput output)
        {
            var targets = new List<PredictionOutput>();

            // main target
            output.AoeTargetsHit = new List<PredictionOutput>
                           {
                               output
                           };
            targets.Add(output);

            foreach (var target in input.AreaOfEffectTargets)
            {
                var targetPrediction = this.GetSimplePrediction(input.WithTarget(target));

                if (this.IsInRange(input, targetPrediction.UnitPosition))
                {
                    targets.Add(targetPrediction);
                }
            }

            switch (input.PredictionSkillshotType)
            {
                case PredictionSkillshotType.SkillshotCircle:

                    if (input.AreaOfEffectHitMainTarget)
                    {
                        while (targets.Count > 1)
                        {
                            var mecResult = MEC.GetMec(targets.Select((target) => target.UnitPosition.ToVector2()).ToList());

                            // add hullradius?
                            if (mecResult.Radius != 0f && mecResult.Radius < input.Radius && this.IsInRange(input, mecResult.Center.ToVector3()))
                            {
                                output.CastPosition = new Vector3(
                                    targets.Count <= 2 ? (targets[0].UnitPosition.ToVector2() + targets[1].UnitPosition.ToVector2()) / 2 : mecResult.Center,
                                    output.CastPosition.Z);
                                output.AoeTargetsHit = targets.Where((target) => output.CastPosition.IsInRange(target.UnitPosition, input.Radius)).ToList();
                                break;
                            }

                            var itemToRemove = targets.MaxOrDefault((target) => targets[0].UnitPosition.DistanceSquared(target.UnitPosition));
                            targets.Remove(itemToRemove);
                        }
                    }
                    else
                    {
                        // TODO: handle the AreaOfEffectHitMainTarget=false case
                    }

                    break;

                case PredictionSkillshotType.SkillshotCone:
                    break;

                case PredictionSkillshotType.SkillshotLine:
                    break;

                default:
                    break;
            }

            return output;
        }

        private PredictionOutput GetSimplePrediction(PredictionInput input)
        {
            var target = input.Target;
            var targetPosition = input.Target.NetworkPosition;
            var caster = input.Owner;

            var totalDelay = caster.TurnTime(targetPosition) + input.Delay + (Game.Ping / 1000f) + 0.06f;
            var totalArrivalTime = totalDelay + (caster.Distance2D(target, true) / input.Speed);

            if (target.NetworkActivity != NetworkActivity.Move)
            {
                if (target.IsStunned() || target.IsRooted())
                {
                    // TODO: check immobile duration
                    return PredictionOutput(target, targetPosition, HitChance.Immobile);
                }

                if (!caster.IsVisibleToEnemies)
                {
                    return PredictionOutput(target, targetPosition, HitChance.High);
                }

                return PredictionOutput(target, targetPosition, HitChance.Medium);
            }

            var rotationDifferenceRad = (target.RotationDifference * (float)Math.PI) / 180f;
            var direction = rotationDifferenceRad != 0f ? Vector2Extensions.Rotated(target.Direction2D(), rotationDifferenceRad) : target.Direction2D();

            if (rotationDifferenceRad != 0f)
            {
                var timeToRotate = target.TurnTime(Math.Abs(rotationDifferenceRad));
                totalDelay -= timeToRotate;
                totalArrivalTime -= timeToRotate;
            }

            if (input.Speed != float.MaxValue)
            {
                var result = Geometry.VectorMovementCollision(
                    targetPosition.ToVector2(),
                    this.ExtendUntilWall(targetPosition, target.Direction2D().ToVector3(), totalArrivalTime * target.MovementSpeed).ToVector2(),
                    target.MovementSpeed,
                    caster.NetworkPosition.ToVector2(),
                    input.Speed,
                    totalDelay);

                if (result.Item2 != Vector2.Zero)
                {
                    totalArrivalTime = result.Item1 + totalDelay;
                    return new PredictionOutput
                           {
                               Unit = input.Target,
                               ArrivalTime = totalArrivalTime,
                               UnitPosition = this.ExtendUntilWall(targetPosition, direction.ToVector3(), totalArrivalTime * target.MovementSpeed),
                               CastPosition = this.ExtendUntilWall(
                                   targetPosition,
                                   direction.ToVector3(),
                                   ((totalArrivalTime * target.MovementSpeed) + 20f) - input.Radius - target.HullRadius / 2.0f),
                               HitChance = !caster.IsVisibleToEnemies ? HitChance.High : HitChance.Medium
                           };
                }
            }

            return new PredictionOutput
                   {
                       Unit = input.Target,
                       ArrivalTime = totalArrivalTime,
                       UnitPosition = this.ExtendUntilWall(targetPosition, direction.ToVector3(), totalArrivalTime * target.MovementSpeed),
                       CastPosition = this.ExtendUntilWall(
                           targetPosition,
                           direction.ToVector3(),
                           ((totalArrivalTime * target.MovementSpeed) + 20f) - input.Radius - target.HullRadius / 2.0f),
                       HitChance = input.Speed != float.MaxValue ? HitChance.Low : HitChance.Medium
                   };
        }

        private bool IsInRange(PredictionInput input, Vector3 Position, bool addRadius = true)
        {
            return input.Owner.IsInRange(Position, input.Range + (input.PredictionSkillshotType == PredictionSkillshotType.SkillshotCircle && addRadius ? input.Radius : 0f));
        }
    }
}