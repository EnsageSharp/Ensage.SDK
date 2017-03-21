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
        public static float Distance2D(this Unit entity, Unit other)
        {
            return entity.Distance2D(other.NetworkPosition) - entity.HullRadius - other.HullRadius;
        }

        public static float Distance2D(this Entity entity, Entity other)
        {
            return entity.Distance2D(other.NetworkPosition);
        }

        public static float Distance2D(this Entity entity, EntityOrPosition other)
        {
            return other.Entity != null && other.Entity.IsValid
                       ? entity.Distance2D(other.Entity)
                       : entity.Distance2D(other.Position);
        }

        public static float Distance2D(this Entity entity, Vector3 position)
        {
            var entityPosition = entity.NetworkPosition;
            return
                (float)
                Math.Sqrt(Math.Pow(entityPosition.X - position.X, 2) + Math.Pow(entityPosition.Y - position.Y, 2));
        }
    }
}