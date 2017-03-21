// <copyright file="IntersectionResult.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Geometry
{
    using SharpDX;

    /// <summary>
    ///     Represents an intersection result.
    /// </summary>
    public struct IntersectionResult
    {
        #region Fields

        /// <summary>
        ///     If they intersect.
        /// </summary>
        public bool Intersects;

        /// <summary>
        ///     The point
        /// </summary>
        public Vector2 Point;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="IntersectionResult" /> struct.
        /// </summary>
        /// <param name="Intersects">if set to <c>true</c>, they insersect.</param>
        /// <param name="Point">The point.</param>
        public IntersectionResult(bool Intersects = false, Vector2 Point = new Vector2())
        {
            this.Intersects = Intersects;
            this.Point = Point;
        }

        #endregion
    }
}