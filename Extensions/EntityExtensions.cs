// <copyright file="EntityExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System;

    using Ensage.SDK.Helpers;

    using SharpDX;

    public static class EntityExtensions
    {
        public static float Distance2D(this Unit entity, Unit other, bool fromCenterToCenter = false)
        {
            return entity.Distance2D(other.NetworkPosition) - (fromCenterToCenter ? 0f : entity.HullRadius + other.HullRadius);
        }

        public static float Distance2D(this Entity entity, Entity other)
        {
            return entity.Distance2D(other.NetworkPosition);
        }

        public static float Distance2D(this Entity entity, EntityOrPosition other)
        {
            return other.Entity != null && other.Entity.IsValid ? entity.Distance2D(other.Entity) : entity.Distance2D(other.Position);
        }

        public static float Distance2D(this Entity entity, Vector3 position)
        {
            var entityPosition = entity.NetworkPosition;
            return (float)Math.Sqrt(Math.Pow(entityPosition.X - position.X, 2) + Math.Pow(entityPosition.Y - position.Y, 2));
        }

        /// <summary>
        ///     Returns if the distance to target is lower than range
        /// </summary>
        /// <param name="sourcePosition"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsInRange(this Entity source, Entity target, float range)
        {
            return source.NetworkPosition.IsInRange(target, range);
        }

        /// <summary>
        ///     Returns if the distance to target is lower than range
        /// </summary>
        /// <param name="sourcePosition"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsInRange(this Entity source, Vector2 targetPosition, float range)
        {
            return source.NetworkPosition.IsInRange(targetPosition, range);
        }

        /// <summary>
        ///     Returns if the distance to target is lower than range
        /// </summary>
        /// <param name="sourcePosition"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsInRange(this Entity source, Vector3 targetPosition, float range)
        {
            return source.NetworkPosition.IsInRange(targetPosition, range);
        }
    }
}