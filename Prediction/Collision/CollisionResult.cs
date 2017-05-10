// <copyright file="CollisionResult.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction.Collision
{
    using System.Collections.Generic;

    public class CollisionResult
    {
        public CollisionResult(List<CollisionObject> collisionObjects)
        {
            this.CollisionObjects = collisionObjects;
        }

        public bool Collides
        {
            get
            {
                return this.CollisionObjects.Count > 0;
            }
        }

        public IReadOnlyCollection<CollisionObject> CollisionObjects { get; }
    }
}