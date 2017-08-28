// <copyright file="WorldPolygon.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Geometry
{
    using System.Collections.Generic;
    using System.Linq;

    using ClipperLib;

    using SharpDX;

    public class WorldPolygon
    {
        /// <summary>
        ///     The points
        /// </summary>
        public List<Vector3> Points = new List<Vector3>();

        public WorldPolygon(params Vector3[] points)
        {
            this.Points = points.ToList();
        }

        /// <summary>
        ///     Adds the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        public void Add(Vector3 point)
        {
            this.Points.Add(point);
        }

        /// <summary>
        ///     Adds the specified polygon.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public void Add(WorldPolygon polygon)
        {
            foreach (var point in polygon.Points)
            {
                this.Points.Add(point);
            }
        }

        /// <summary>
        ///     Draws the polygon.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="width">The width.</param>
        public virtual void Draw(Color color, int width = 1)
        {
            for (var i = 0; i <= (this.Points.Count - 1); i++)
            {
                var nextIndex = this.Points.Count - 1 == i ? 0 : i + 1;

                var fromTmp = new Vector3(this.Points[i].X, this.Points[i].Y, this.Points[i].Z);
                var toTmp = new Vector3(this.Points[nextIndex].X, this.Points[nextIndex].Y, this.Points[nextIndex].Z);

                var from = Drawing.WorldToScreen(fromTmp);
                var to = Drawing.WorldToScreen(toTmp);
                Drawing.DrawLine(from, to, color);
            }
        }

        /// <summary>
        ///     Determines whether the specified point is inside.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool IsInside(Vector3 point)
        {
            return !this.IsOutside(point);
        }

        /// <summary>
        ///     Determines whether the specified point is outside.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool IsOutside(Vector3 point)
        {
            var p = new IntPoint(point.X, point.Y, point.Z);
            return Clipper.PointInPolygon(p, this.ToClipperPath()) != 1;
        }

        /// <summary>
        ///     Converts this instance to a clipper path.
        /// </summary>
        /// <returns></returns>
        public List<IntPoint> ToClipperPath()
        {
            var result = new List<IntPoint>(this.Points.Count);
            result.AddRange(this.Points.Select(point => new IntPoint(point.X, point.Y, point.Z)));
            return result;
        }
    }
}