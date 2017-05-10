// <copyright file="CollisionObject.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction.Collision
{
    using Ensage.SDK.Extensions;

    using SharpDX;

    public class CollisionObject
    {
        public CollisionObject(Unit unit)
        {
            this.Entity = unit;
            this.Position = unit.NetworkPosition.ToVector2();
            this.Radius = unit.HullRadius;
        }

        public CollisionObject(Entity entity, Vector2 position, float radius)
        {
            this.Entity = entity;
            this.Position = position;
            this.Radius = radius;
        }

        public CollisionObject(Entity entity, Vector3 position, float radius)
        {
            this.Entity = entity;
            this.Position = position.ToVector2();
            this.Radius = radius;
        }

        public Entity Entity { get; }

        public Vector2 Position { get; }

        public float Radius { get; }
    }
}