namespace Ensage.SDK.Extensions
{
    using SharpDX;

    /// <summary>
    ///     Holds info for the <see cref="Vector2Extensions.ProjectOn" /> method.
    /// </summary>
    public struct ProjectionInfo
    {
        #region Fields

        /// <summary>
        ///     Returns if the point is on the segment
        /// </summary>
        public bool IsOnSegment;

        /// <summary>
        ///     Line point
        /// </summary>
        public Vector2 LinePoint;

        /// <summary>
        ///     Segment point
        /// </summary>
        public Vector2 SegmentPoint;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectionInfo" /> struct.
        /// </summary>
        /// <param name="isOnSegment">
        ///     Is on Segment
        /// </param>
        /// <param name="segmentPoint">
        ///     Segment point
        /// </param>
        /// <param name="linePoint">
        ///     Line point
        /// </param>
        internal ProjectionInfo(bool isOnSegment, Vector2 segmentPoint, Vector2 linePoint)
        {
            this.IsOnSegment = isOnSegment;
            this.SegmentPoint = segmentPoint;
            this.LinePoint = linePoint;
        }

        #endregion
    }
}