// <copyright file="Polygon.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ClipperLib;

    using SharpDX;

    /// <summary>
    ///     Represents a polygon.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        ///     The points
        /// </summary>
        public List<Vector2> Points = new List<Vector2>();

        /// <summary>
        ///     Adds the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        public void Add(Vector2 point)
        {
            this.Points.Add(point);
        }

        /// <summary>
        ///     Adds the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        public void Add(Vector3 point)
        {
            this.Points.Add(point.To2D());
        }

        /// <summary>
        ///     Adds the specified polygon.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public void Add(Polygon polygon)
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
            for (var i = 0; i <= this.Points.Count - 1; i++)
            {
                var nextIndex = this.Points.Count - 1 == i ? 0 : i + 1;

                var fromTmp = new Vector3(this.Points[i].X, this.Points[i].Y, 0);
                var toTmp = new Vector3(this.Points[nextIndex].X, this.Points[nextIndex].Y, 0);

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
        public bool IsInside(Vector2 point)
        {
            return !this.IsOutside(point);
        }

        /// <summary>
        ///     Determines whether the specified point is inside.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool IsInside(Vector3 point)
        {
            return !this.IsOutside(point.To2D());
        }

        /// <summary>
        ///     Determines whether the specified point is outside.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public bool IsOutside(Vector2 point)
        {
            var p = new IntPoint(point.X, point.Y);
            return Clipper.PointInPolygon(p, this.ToClipperPath()) != 1;
        }

        /// <summary>
        ///     Converts this instance to a clipper path.
        /// </summary>
        /// <returns></returns>
        public List<IntPoint> ToClipperPath()
        {
            var result = new List<IntPoint>(this.Points.Count);
            result.AddRange(this.Points.Select(point => new IntPoint(point.X, point.Y)));
            return result;
        }

        /// <summary>
        ///     Represnets an arc polygon.
        /// </summary>
        public class Arc : Polygon
        {
            /// <summary>
            ///     The angle
            /// </summary>
            public float Angle;

            /// <summary>
            ///     The end position
            /// </summary>
            public Vector2 EndPos;

            /// <summary>
            ///     The radius
            /// </summary>
            public float Radius;

            /// <summary>
            ///     The start position
            /// </summary>
            public Vector2 StartPos;

            /// <summary>
            ///     The quality
            /// </summary>
            private readonly int _quality;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Arc" /> class.
            /// </summary>
            /// <param name="start">The start.</param>
            /// <param name="direction">The direction.</param>
            /// <param name="angle">The angle.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="quality">The quality.</param>
            public Arc(Vector3 start, Vector3 direction, float angle, float radius, int quality = 20)
                : this(start.To2D(), direction.To2D(), angle, radius, quality)
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Arc" /> class.
            /// </summary>
            /// <param name="start">The start.</param>
            /// <param name="direction">The direction.</param>
            /// <param name="angle">The angle.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="quality">The quality.</param>
            public Arc(Vector2 start, Vector2 direction, float angle, float radius, int quality = 20)
            {
                this.StartPos = start;
                this.EndPos = (direction - start).Normalized();
                this.Angle = angle;
                this.Radius = radius;
                this._quality = quality;
                this.UpdatePolygon();
            }

            /// <summary>
            ///     Updates the polygon.
            /// </summary>
            /// <param name="offset">The offset.</param>
            public void UpdatePolygon(int offset = 0)
            {
                this.Points.Clear();
                var outRadius = (this.Radius + offset) / (float)Math.Cos(2 * Math.PI / this._quality);
                var side1 = this.EndPos.Rotated(-this.Angle * 0.5f);
                for (var i = 0; i <= this._quality; i++)
                {
                    var cDirection = side1.Rotated(i * this.Angle / this._quality).Normalized();
                    this.Points.Add(
                        new Vector2(
                            this.StartPos.X + outRadius * cDirection.X,
                            this.StartPos.Y + outRadius * cDirection.Y));
                }
            }
        }

        /// <summary>
        ///     Represents a circle polygon.
        /// </summary>
        public class Circle : Polygon
        {
            /// <summary>
            ///     The center
            /// </summary>
            public Vector2 Center;

            /// <summary>
            ///     The radius
            /// </summary>
            public float Radius;

            /// <summary>
            ///     The quality
            /// </summary>
            private readonly int _quality;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Circle" /> class.
            /// </summary>
            /// <param name="center">The center.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="quality">The quality.</param>
            public Circle(Vector3 center, float radius, int quality = 20)
                : this(center.To2D(), radius, quality)
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Circle" /> class.
            /// </summary>
            /// <param name="center">The center.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="quality">The quality.</param>
            public Circle(Vector2 center, float radius, int quality = 20)
            {
                this.Center = center;
                this.Radius = radius;
                this._quality = quality;
                this.UpdatePolygon();
            }

            /// <summary>
            ///     Updates the polygon.
            /// </summary>
            /// <param name="offset">The offset.</param>
            /// <param name="overrideWidth">Width of the override.</param>
            public void UpdatePolygon(int offset = 0, float overrideWidth = -1)
            {
                this.Points.Clear();
                var outRadius = overrideWidth > 0
                                    ? overrideWidth
                                    : (offset + this.Radius) / (float)Math.Cos(2 * Math.PI / this._quality);
                for (var i = 1; i <= this._quality; i++)
                {
                    var angle = i * 2 * Math.PI / this._quality;
                    var point = new Vector2(
                        this.Center.X + outRadius * (float)Math.Cos(angle),
                        this.Center.Y + outRadius * (float)Math.Sin(angle));
                    this.Points.Add(point);
                }
            }
        }

        public class Cone : Rectangle
        {
            public float EndWidth;

            public Cone(Vector3 start, Vector3 end, float startWidth, float endWidth)
                : base(start, end, startWidth)
            {
                this.EndWidth = endWidth;
            }

            public Cone(Vector2 start, Vector2 end, float startWidth, float endWidth)
                : base(start, end, startWidth)
            {
                this.EndWidth = endWidth;
            }
        }

        /// <summary>
        ///     Represents a line polygon.
        /// </summary>
        public class Line : Polygon
        {
            /// <summary>
            ///     The line end
            /// </summary>
            public Vector2 LineEnd;

            /// <summary>
            ///     The line start
            /// </summary>
            public Vector2 LineStart;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Line" /> class.
            /// </summary>
            /// <param name="start">The start.</param>
            /// <param name="end">The end.</param>
            /// <param name="length">The length.</param>
            public Line(Vector3 start, Vector3 end, float length = -1)
                : this(start.To2D(), end.To2D(), length)
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Line" /> class.
            /// </summary>
            /// <param name="start">The start.</param>
            /// <param name="end">The end.</param>
            /// <param name="length">The length.</param>
            public Line(Vector2 start, Vector2 end, float length = -1)
            {
                this.LineStart = start;
                this.LineEnd = end;
                if (length > 0)
                {
                    this.Length = length;
                }

                this.UpdatePolygon();
            }

            /// <summary>
            ///     Gets or sets the length.
            /// </summary>
            /// <value>
            ///     The length.
            /// </value>
            public float Length
            {
                get
                {
                    return this.LineStart.Distance(this.LineEnd);
                }

                set
                {
                    this.LineEnd = (this.LineEnd - this.LineStart).Normalized() * value + this.LineStart;
                }
            }

            /// <summary>
            ///     Updates the polygon.
            /// </summary>
            public void UpdatePolygon()
            {
                this.Points.Clear();
                this.Points.Add(this.LineStart);
                this.Points.Add(this.LineEnd);
            }
        }

        /// <summary>
        ///     Represents a rectangle polygon.
        /// </summary>
        public class Rectangle : Polygon
        {
            /// <summary>
            ///     The end
            /// </summary>
            public Vector2 End;

            /// <summary>
            ///     The start
            /// </summary>
            public Vector2 Start;

            /// <summary>
            ///     The width
            /// </summary>
            public float Width;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Rectangle" /> class.
            /// </summary>
            /// <param name="start">The start.</param>
            /// <param name="end">The end.</param>
            /// <param name="width">The width.</param>
            public Rectangle(Vector3 start, Vector3 end, float width)
                : this(start.To2D(), end.To2D(), width)
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="Rectangle" /> class.
            /// </summary>
            /// <param name="start">The start.</param>
            /// <param name="end">The end.</param>
            /// <param name="width">The width.</param>
            public Rectangle(Vector2 start, Vector2 end, float width)
            {
                this.Start = start;
                this.End = end;
                this.Width = width;
                this.UpdatePolygon();
            }

            /// <summary>
            ///     Gets the direction.
            /// </summary>
            /// <value>
            ///     The direction.
            /// </value>
            public Vector2 Direction
            {
                get
                {
                    return (this.End - this.Start).Normalized();
                }
            }

            /// <summary>
            ///     Gets the perpendicular.
            /// </summary>
            /// <value>
            ///     The perpendicular.
            /// </value>
            public Vector2 Perpendicular
            {
                get
                {
                    return this.Direction.Perpendicular();
                }
            }

            /// <summary>
            ///     Updates the polygon.
            /// </summary>
            /// <param name="offset">The offset.</param>
            /// <param name="overrideWidth">Width of the override.</param>
            public void UpdatePolygon(int offset = 0, float overrideWidth = -1)
            {
                this.Points.Clear();
                this.Points.Add(
                    this.Start + (overrideWidth > 0 ? overrideWidth : this.Width + offset) * this.Perpendicular
                    - offset * this.Direction);
                this.Points.Add(
                    this.Start - (overrideWidth > 0 ? overrideWidth : this.Width + offset) * this.Perpendicular
                    - offset * this.Direction);
                this.Points.Add(
                    this.End - (overrideWidth > 0 ? overrideWidth : this.Width + offset) * this.Perpendicular
                    + offset * this.Direction);
                this.Points.Add(
                    this.End + (overrideWidth > 0 ? overrideWidth : this.Width + offset) * this.Perpendicular
                    + offset * this.Direction);
            }
        }

        /// <summary>
        ///     Represents a ring polygon.
        /// </summary>
        public class Ring : Polygon
        {
            /// <summary>
            ///     The center
            /// </summary>
            public Vector2 Center;

            /// <summary>
            ///     The inner radius
            /// </summary>
            public float InnerRadius;

            /// <summary>
            ///     The outer radius
            /// </summary>
            public float OuterRadius;

            /// <summary>
            ///     The quality
            /// </summary>
            private readonly int _quality;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Ring" /> class.
            /// </summary>
            /// <param name="center">The center.</param>
            /// <param name="innerRadius">The inner radius.</param>
            /// <param name="outerRadius">The outer radius.</param>
            /// <param name="quality">The quality.</param>
            public Ring(Vector3 center, float innerRadius, float outerRadius, int quality = 20)
                : this(center.To2D(), innerRadius, outerRadius, quality)
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Ring" /> class.
            /// </summary>
            /// <param name="center">The center.</param>
            /// <param name="innerRadius">The inner radius.</param>
            /// <param name="outerRadius">The outer radius.</param>
            /// <param name="quality">The quality.</param>
            public Ring(Vector2 center, float innerRadius, float outerRadius, int quality = 20)
            {
                this.Center = center;
                this.InnerRadius = innerRadius;
                this.OuterRadius = outerRadius;
                this._quality = quality;
                this.UpdatePolygon();
            }

            /// <summary>
            ///     Updates the polygon.
            /// </summary>
            /// <param name="offset">The offset.</param>
            public void UpdatePolygon(int offset = 0)
            {
                this.Points.Clear();
                var outRadius = (offset + this.InnerRadius + this.OuterRadius)
                                / (float)Math.Cos(2 * Math.PI / this._quality);
                var innerRadius = this.InnerRadius - this.OuterRadius - offset;
                for (var i = 0; i <= this._quality; i++)
                {
                    var angle = i * 2 * Math.PI / this._quality;
                    var point = new Vector2(
                        this.Center.X - outRadius * (float)Math.Cos(angle),
                        this.Center.Y - outRadius * (float)Math.Sin(angle));
                    this.Points.Add(point);
                }

                for (var i = 0; i <= this._quality; i++)
                {
                    var angle = i * 2 * Math.PI / this._quality;
                    var point = new Vector2(
                        this.Center.X + innerRadius * (float)Math.Cos(angle),
                        this.Center.Y - innerRadius * (float)Math.Sin(angle));
                    this.Points.Add(point);
                }
            }
        }

        /// <summary>
        ///     Represnets a sector polygon.
        /// </summary>
        public class Sector : Polygon
        {
            /// <summary>
            ///     The angle
            /// </summary>
            public float Angle;

            /// <summary>
            ///     The center
            /// </summary>
            public Vector2 Center;

            /// <summary>
            ///     The direction
            /// </summary>
            public Vector2 Direction;

            /// <summary>
            ///     The radius
            /// </summary>
            public float Radius;

            /// <summary>
            ///     The quality
            /// </summary>
            private readonly int _quality;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Sector" /> class.
            /// </summary>
            /// <param name="center">The center.</param>
            /// <param name="direction">The direction.</param>
            /// <param name="angle">The angle.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="quality">The quality.</param>
            public Sector(Vector3 center, Vector3 direction, float angle, float radius, int quality = 20)
                : this(center.To2D(), direction.To2D(), angle, radius, quality)
            {
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="Polygon.Sector" /> class.
            /// </summary>
            /// <param name="center">The center.</param>
            /// <param name="direction">The direction.</param>
            /// <param name="angle">The angle.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="quality">The quality.</param>
            public Sector(Vector2 center, Vector2 direction, float angle, float radius, int quality = 20)
            {
                this.Center = center;
                this.Direction = (direction - center).Normalized();
                this.Angle = angle;
                this.Radius = radius;
                this._quality = quality;
                this.UpdatePolygon();
            }

            /// <summary>
            ///     Rotates Line by angle/radian
            /// </summary>
            /// <param name="point1"></param>
            /// <param name="point2"></param>
            /// <param name="value"></param>
            /// <param name="radian">True for radian values, false for degree</param>
            /// <returns></returns>
            public Vector2 RotateLineFromPoint(Vector2 point1, Vector2 point2, float value, bool radian = true)
            {
                var angle = !radian ? value * Math.PI / 180 : value;
                var line = Vector2.Subtract(point2, point1);

                var newline = new Vector2
                                  {
                                      X = (float)(line.X * Math.Cos(angle) - line.Y * Math.Sin(angle)),
                                      Y = (float)(line.X * Math.Sin(angle) + line.Y * Math.Cos(angle))
                                  };

                return Vector2.Add(newline, point1);
            }

            /// <summary>
            ///     Updates the polygon.
            /// </summary>
            /// <param name="offset">The offset.</param>
            public void UpdatePolygon(int offset = 0)
            {
                this.Points.Clear();
                var outRadius = (this.Radius + offset) / (float)Math.Cos(2 * Math.PI / this._quality);
                this.Points.Add(this.Center);
                var side1 = this.Direction.Rotated(-this.Angle * 0.5f);
                for (var i = 0; i <= this._quality; i++)
                {
                    var cDirection = side1.Rotated(i * this.Angle / this._quality).Normalized();
                    this.Points.Add(
                        new Vector2(
                            this.Center.X + outRadius * cDirection.X,
                            this.Center.Y + outRadius * cDirection.Y));
                }
            }
        }
    }
}