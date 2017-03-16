using System;
using SharpDX;

namespace Ensage.SDK.Extensions
{
    public static class EntityExtensions
    {
        public static float Distance2D(this Unit entity, Unit other)
        {
            return entity.Distance2D(other.NetworkPosition) - entity.HullRadius - other.HullRadius;
        }

        public static float Distance2D(this Entity entity, Vector3 position)
        {
            var entityPosition = entity.NetworkPosition;
            return (float)
                Math.Sqrt(Math.Pow(entityPosition.X - position.X, 2) +
                          Math.Pow(entityPosition.Y - position.Y, 2));
        }
    }
}