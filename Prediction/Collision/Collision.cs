// <copyright file="Collision.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction.Collision
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Geometry;

    using SharpDX;

    public static class Collision
    {
        public static CollisionResult GetCollision(Vector2 startPosition, Vector2 endPosition, float radius, List<CollisionObject> collisionObjects)
        {
            var objects = new List<CollisionObject>();
            var buffer = 0f;
            foreach (var obj in collisionObjects)
            {
                if (obj.Position.Distance(startPosition, endPosition, true, true) < ((radius + obj.Radius + buffer) * (radius + obj.Radius + buffer)))
                {
                    objects.Add(obj);
                }
            }

            objects = objects.OrderBy((obj) => startPosition.Distance(obj.Position)).ToList();

            return new CollisionResult(objects);
        }
    }
}