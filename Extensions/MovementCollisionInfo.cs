// <copyright file="MovementCollisionInfo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using SharpDX;

    /// <summary>
    ///     Holds info for the VectorMovementCollision method.
    /// </summary>
    public struct MovementCollisionInfo
    {
        #region Fields

        /// <summary>
        ///     Collision position.
        /// </summary>
        public Vector2 CollisionPosition;

        /// <summary>
        ///     Collision Time from calculation.
        /// </summary>
        public float CollisionTime;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MovementCollisionInfo" /> struct.
        /// </summary>
        /// <param name="collisionTime">
        ///     Collision time from calculation
        /// </param>
        /// <param name="collisionPosition">
        ///     Collision position
        /// </param>
        internal MovementCollisionInfo(float collisionTime, Vector2 collisionPosition)
        {
            this.CollisionTime = collisionTime;
            this.CollisionPosition = collisionPosition;
        }

        #endregion

        #region Public Indexers

        /// <summary>
        ///     Information accessor.
        /// </summary>
        /// <param name="i">
        ///     The Indexer.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        public object this[int i]
        {
            get
            {
                return i == 0 ? this.CollisionTime : (object)this.CollisionPosition;
            }
        }

        #endregion
    }
}