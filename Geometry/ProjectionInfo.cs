// <copyright file="ProjectionInfo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Geometry
{
    using SharpDX;

    /// <summary>
    ///     Represents the projection information.
    /// </summary>
    public struct ProjectionInfo
    {
        #region Fields

        /// <summary>
        ///     The is on segment
        /// </summary>
        public bool IsOnSegment;

        /// <summary>
        ///     The line point
        /// </summary>
        public Vector2 LinePoint;

        /// <summary>
        ///     The segment point
        /// </summary>
        public Vector2 SegmentPoint;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectionInfo" /> struct.
        /// </summary>
        /// <param name="isOnSegment">if set to <c>true</c> [is on segment].</param>
        /// <param name="segmentPoint">The segment point.</param>
        /// <param name="linePoint">The line point.</param>
        public ProjectionInfo(bool isOnSegment, Vector2 segmentPoint, Vector2 linePoint)
        {
            this.IsOnSegment = isOnSegment;
            this.SegmentPoint = segmentPoint;
            this.LinePoint = linePoint;
        }

        #endregion
    }
}