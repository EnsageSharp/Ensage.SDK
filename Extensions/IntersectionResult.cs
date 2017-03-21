// <copyright file="IntersectionResult.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using SharpDX;

    /// <summary>
    ///     Holds info for the <see cref="Vector2Extensions.Intersection" /> method.
    /// </summary>
    public struct IntersectionResult
    {
        #region Fields

        /// <summary>
        ///     Returns if both of the points intersect.
        /// </summary>
        public bool Intersects;

        /// <summary>
        ///     Intersection point
        /// </summary>
        public Vector2 Point;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntersectionResult" /> struct.
        ///     Constructor for Intersection Result
        /// </summary>
        /// <param name="intersects">
        ///     Intersection of input
        /// </param>
        /// <param name="point">
        ///     Intersection Point
        /// </param>
        public IntersectionResult(bool intersects = false, Vector2 point = new Vector2())
        {
            this.Intersects = intersects;
            this.Point = point;
        }

        #endregion
    }
}