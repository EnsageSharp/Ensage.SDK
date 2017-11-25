// <copyright file="Prediction.cs" company="Ensage">
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
    public sealed class Prediction : IPrediction
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

            result = this.GetProperCastPosition(input, result);

            // check range
            if (input.Range != float.MaxValue)
            {
                if (!this.IsInRange(input, result.CastPosition, false) && !this.IsInRange(input, result.UnitPosition, true))
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

                if ((input.CollisionTypes & CollisionTypes.AllyCreeps) == CollisionTypes.AllyCreeps)
                {
                    movingObjects.AddRange(
                        EntityManager<Creep>.Entities.Where(
                            unit => unit.IsAlly(input.Owner) && unit.IsValidTarget(float.MaxValue, false) && input.Owner.IsInRange(unit, scanRange)));
                }

                if ((input.CollisionTypes & CollisionTypes.EnemyCreeps) == CollisionTypes.EnemyCreeps)
                {
                    movingObjects.AddRange(
                        EntityManager<Creep>.Entities.Where(
                            unit => unit.IsEnemy(input.Owner) && unit.IsValidTarget(float.MaxValue, false) && input.Owner.IsInRange(unit, scanRange)));
                }

                if ((input.CollisionTypes & CollisionTypes.AllyHeroes) == CollisionTypes.AllyHeroes)
                {
                    movingObjects.AddRange(
                        EntityManager<Hero>.Entities.Where(
                            unit => unit.IsAlly(input.Owner)
                                    && unit.IsValidTarget(float.MaxValue, false)
                                    && input.Owner.IsInRange(unit, scanRange)
                                    && unit != input.Owner));
                }

                if ((input.CollisionTypes & CollisionTypes.EnemyHeroes) == CollisionTypes.EnemyHeroes)
                {
                    movingObjects.AddRange(
                        EntityManager<Hero>.Entities.Where(
                            unit => unit.IsEnemy(input.Owner)
                                    && unit.IsValidTarget(float.MaxValue, false)
                                    && input.Owner.IsInRange(unit, scanRange)
                                    && unit != input.Target));
                }

                // add units
                foreach (var unit in movingObjects)
                {
                    var predictedPos = this.GetSimplePrediction(input.WithTarget(unit));
                    collisionObjects.Add(new CollisionObject(unit, predictedPos.UnitPosition, unit.HullRadius + 10f));
                    collisionObjects.Add(new CollisionObject(unit, unit.NetworkPosition, unit.HullRadius)); // optional
                }

                // add trees and buildings, use NavMeshCellFlags for less lag?
                if ((input.CollisionTypes & CollisionTypes.Trees) == CollisionTypes.Trees)
                {
                    foreach (var tree in EntityManager<Tree>.Entities.Where(unit => input.Owner.IsInRange(unit, scanRange)))
                    {
                        collisionObjects.Add(new CollisionObject(tree, tree.NetworkPosition, 75f));
                    }
                }

                // runes for pudge
                if ((input.CollisionTypes & CollisionTypes.Runes) == CollisionTypes.Runes)
                {
                    foreach (var rune in EntityManager<Rune>.Entities.Where(unit => input.Owner.IsInRange(unit, scanRange)))
                    {
                        collisionObjects.Add(new CollisionObject(rune, rune.NetworkPosition, 75f));
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

            while (distance > 0f && (this.Pathfinder.GetCellFlags(testPoint) & NavMeshCellFlags.Walkable) == NavMeshCellFlags.Walkable)
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

            // aoe targets
            foreach (var target in input.AreaOfEffectTargets.Where(e => e.Handle != output.Unit.Handle))
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
                        // main target
                        targets.Insert(0, output);

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

        private PredictionOutput GetProperCastPosition(PredictionInput input, PredictionOutput output)
        {
            var castPosition = output.CastPosition;
            var distance = input.Owner.Distance2D(castPosition);
            var caster = input.Owner;
            var range = input.Range;
            var radius = input.Radius;

            if (radius > 0 && distance > range)
            {
                output.CastPosition = castPosition.Extend(caster.NetworkPosition, Math.Min(caster.Distance2D(castPosition) - range, radius));
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
                    // TODO: test this immobile duration
                    // var immobileDuration = target.ImmobileDuration();

                    //// check if enemy could run out of the radius
                    // if (totalArrivalTime - (input.Radius / target.MovementSpeed) > immobileDuration)
                    // {
                    // // assume enemy will run in their facing direction 
                    // return new PredictionOutput
                    // {
                    // Unit = input.Target,
                    // ArrivalTime = totalArrivalTime,
                    // UnitPosition = this.ExtendUntilWall(targetPosition, direction.ToVector3(),
                    // (totalArrivalTime - immobileDuration) * target.MovementSpeed),
                    // CastPosition =
                    // this.ExtendUntilWall(
                    // targetPosition,
                    // direction.ToVector3(),
                    // (((totalArrivalTime - immobileDuration) * target.MovementSpeed) + 20f) - input.Radius - (target.HullRadius / 2.0f)),
                    // HitChance = HitChance.Medium
                    // };
                    // }
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
                        CastPosition = this.ExtendUntilWall(targetPosition, direction.ToVector3(), ((totalArrivalTime * target.MovementSpeed) + 20f) - (target.HullRadius / 2.0f)),
                        HitChance = !caster.IsVisibleToEnemies ? HitChance.High : HitChance.Medium
                    };
                }
            }

            return new PredictionOutput
            {
                Unit = input.Target,
                ArrivalTime = totalArrivalTime,
                UnitPosition = this.ExtendUntilWall(targetPosition, direction.ToVector3(), totalArrivalTime * target.MovementSpeed),
                CastPosition = this.ExtendUntilWall(targetPosition, direction.ToVector3(), ((totalArrivalTime * target.MovementSpeed) + 20f) - (target.HullRadius / 2.0f)),
                HitChance = input.Speed != float.MaxValue ? HitChance.Low : HitChance.Medium
            };
        }

        private bool IsInRange(PredictionInput input, Vector3 position, bool addRadius = true)
        {
            return input.Owner.IsInRange(position, input.Range + (addRadius ? input.Radius : 0f));
        }
    }
}