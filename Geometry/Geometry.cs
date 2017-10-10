// <copyright file="Geometry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ClipperLib;

    using Ensage.SDK.Extensions;

    using SharpDX;

    /// <summary>
    ///     Provides methods regarding geometry math.
    /// </summary>
    public static class Geometry
    {
        /// <summary>
        ///     Returns the angle with the vector p2 in degrees;
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <returns></returns>
        public static float AngleBetween(this Vector2 p1, Vector2 p2)
        {
            var theta = p1.Polar() - p2.Polar();
            if (theta < 0)
            {
                theta = theta + 360;
            }

            if (theta > 180)
            {
                theta = 360 - theta;
            }

            return theta;
        }

        /// <summary>
        ///     Returns a Vector2 at center of the polygone.
        /// </summary>
        /// <param name="p">The polygon.</param>
        /// <returns></returns>
        public static Vector2 CenterOfPolygone(this Polygon p)
        {
            var cX = 0f;
            var cY = 0f;
            var pc = p.Points.Count;
            foreach (var point in p.Points)
            {
                cX += point.X;
                cY += point.Y;
            }

            return new Vector2(cX / pc, cY / pc);
        }

        /// <summary>
        ///     Returns the two intersection points between two circles.
        /// </summary>
        /// <param name="center1">The center1.</param>
        /// <param name="center2">The center2.</param>
        /// <param name="radius1">The radius1.</param>
        /// <param name="radius2">The radius2.</param>
        /// <returns></returns>
        public static Vector2[] CircleCircleIntersection(Vector2 center1, Vector2 center2, float radius1, float radius2)
        {
            var D = center1.Distance2D(center2);

            // The Circles dont intersect:
            if (D > (radius1 + radius2) || D <= Math.Abs(radius1 - radius2))
            {
                return new Vector2[]
                       {
                       };
            }

            var A = (((radius1 * radius1) - (radius2 * radius2)) + (D * D)) / (2 * D);
            var H = (float)Math.Sqrt((radius1 * radius1) - (A * A));
            var Direction = (center2 - center1).Normalized();
            var PA = center1 + (A * Direction);
            var S1 = PA + (H * Direction.Perpendicular());
            var S2 = PA - (H * Direction.Perpendicular());
            return new[]
                   {
                       S1,
                       S2
                   };
        }

        /// <summary>
        ///     Clips the polygons.
        /// </summary>
        /// <param name="polygons">The polygons.</param>
        /// <returns></returns>
        public static List<List<IntPoint>> ClipPolygons(List<Polygon> polygons)
        {
            var subj = new List<List<IntPoint>>(polygons.Count);
            var clip = new List<List<IntPoint>>(polygons.Count);
            foreach (var polygon in polygons)
            {
                subj.Add(polygon.ToClipperPath());
                clip.Add(polygon.ToClipperPath());
            }

            var solution = new List<List<IntPoint>>();
            var c = new Clipper();
            c.AddPaths(subj, PolyType.ptSubject, true);
            c.AddPaths(clip, PolyType.ptClip, true);
            c.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftEvenOdd);
            return solution;
        }

        /// <summary>
        ///     Checks if the two floats are close to each other.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="eps">The epsilon.</param>
        /// <returns></returns>
        public static bool Close(float a, float b, float eps)
        {
            if (Math.Abs(eps) < float.Epsilon)
            {
                eps = (float)1e-9;
            }

            return Math.Abs(a - b) <= eps;
        }

        /// <summary>
        ///     Returns the closest vector from a list.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="vList">The v list.</param>
        /// <returns></returns>
        public static Vector2 Closest(this Vector2 v, List<Vector2> vList)
        {
            var result = new Vector2();
            var dist = float.MaxValue;

            foreach (var vector in vList)
            {
                var distance = Vector2.DistanceSquared(v, vector);
                if (distance < dist)
                {
                    dist = distance;
                    result = vector;
                }
            }

            return result;
        }

        public static Vector3 GetClosestPoint(List<Vector3> route, Vector3 v)
        {
            int tmp;
            return GetClosestPoint(route, v, out tmp);
        }

        public static Vector3 GetClosestPoint(List<Vector3> route, Vector3 v, out int index)
        {
            index = 0;
            Vector3 bestResult = v;
            var bestDistance = float.MaxValue;
            for (var i = 0; i < route.Count - 1; ++i)
            {
                var p1 = route[i];
                var p2 = route[i + 1];

                var AP = v - p1; 
                var AB = p2 - p1; 

                var magnitudeAB = AB.LengthSquared();  
                var ABAPproduct = Vector3.Dot(AP, AB); 
                var distance = ABAPproduct / magnitudeAB;

                if (distance < 0)
                {
                    distance = AP.Length();
                    if (bestDistance < distance)
                    {
                        bestDistance = distance;
                        bestResult = p1;
                        index = i;
                    }
                }
                else if (distance > 1)
                {
                    distance = (v - p2).Length();
                    if (bestDistance < distance)
                    {
                        bestDistance = distance;
                        bestResult = p2;
                        index = i + 1;
                    }
                }
                else
                {
                    var p = p1 + (AB * distance);
                    distance = (v - p).Length();
                    if (bestDistance < distance)
                    {
                        bestDistance = distance;
                        bestResult = p;
                        index = i;
                    }
                }
            }

            return bestResult;
        }

        /// <summary>
        ///     Returns the cross product Z value.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public static float CrossProduct(this Vector2 self, Vector2 other)
        {
            return (other.Y * self.X) - (other.X * self.Y);
        }

        /// <summary>
        ///     Converts degrees to radians.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static float DegreeToRadian(double angle)
        {
            return (float)((Math.PI * angle) / 180.0);
        }

        /// <summary>
        ///     Returns the 2D distance (XY plane) between two vector.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="other">The other.</param>
        /// <param name="squared">if set to <c>true</c> [squared].</param>
        /// <returns></returns>
        public static float Distance2D(this Vector3 v, Vector3 other, bool squared = false)
        {
            return v.To2D().Distance2D(other, squared);
        }

        /// <summary>
        ///     Calculates the distance to the Vector2.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="to">To.</param>
        /// <param name="squared">if set to <c>true</c> gets the distance squared.</param>
        /// <returns></returns>
        public static float Distance2D(this Vector2 v, Vector2 to, bool squared = false)
        {
            return squared ? Vector2.DistanceSquared(v, to) : Vector2.Distance(v, to);
        }

        /// <summary>
        ///     Calculates the distance to the Vector3.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="to">To.</param>
        /// <param name="squared">if set to <c>true</c> gets the distance squared.</param>
        /// <returns></returns>
        public static float Distance2D(this Vector2 v, Vector3 to, bool squared = false)
        {
            return v.Distance2D(to.To2D(), squared);
        }

        public static float Distance2D(this Vector2 point, Vector2 segmentStart, Vector2 segmentEnd, bool onlyIfOnSegment = false, bool squared = false)
        {
            var objects = point.ProjectOn(segmentStart, segmentEnd);

            if (objects.IsOnSegment || onlyIfOnSegment == false)
            {
                return squared ? Vector2.DistanceSquared(objects.SegmentPoint, point) : Vector2.Distance(objects.SegmentPoint, point);
            }

            return float.MaxValue;
        }

        // From: http://social.msdn.microsoft.com/Forums/vstudio/en-US/e5993847-c7a9-46ec-8edc-bfb86bd689e3/help-on-line-segment-intersection-algorithm
        /// <summary>
        ///     Intersects two line segments.
        /// </summary>
        /// <param name="lineSegment1Start">The line segment1 start.</param>
        /// <param name="lineSegment1End">The line segment1 end.</param>
        /// <param name="lineSegment2Start">The line segment2 start.</param>
        /// <param name="lineSegment2End">The line segment2 end.</param>
        /// <returns></returns>
        public static IntersectionResult Intersection(this Vector2 lineSegment1Start, Vector2 lineSegment1End, Vector2 lineSegment2Start, Vector2 lineSegment2End)
        {
            double deltaACy = lineSegment1Start.Y - lineSegment2Start.Y;
            double deltaDCx = lineSegment2End.X - lineSegment2Start.X;
            double deltaACx = lineSegment1Start.X - lineSegment2Start.X;
            double deltaDCy = lineSegment2End.Y - lineSegment2Start.Y;
            double deltaBAx = lineSegment1End.X - lineSegment1Start.X;
            double deltaBAy = lineSegment1End.Y - lineSegment1Start.Y;

            var denominator = (deltaBAx * deltaDCy) - (deltaBAy * deltaDCx);
            var numerator = (deltaACy * deltaDCx) - (deltaACx * deltaDCy);

            if (Math.Abs(denominator) < float.Epsilon)
            {
                if (Math.Abs(numerator) < float.Epsilon)
                {
                    // collinear. Potentially infinite intersection points.
                    // Check and return one of them.
                    if (lineSegment1Start.X >= lineSegment2Start.X && lineSegment1Start.X <= lineSegment2End.X)
                    {
                        return new IntersectionResult(true, lineSegment1Start);
                    }

                    if (lineSegment2Start.X >= lineSegment1Start.X && lineSegment2Start.X <= lineSegment1End.X)
                    {
                        return new IntersectionResult(true, lineSegment2Start);
                    }

                    return new IntersectionResult();
                }

                // parallel
                return new IntersectionResult();
            }

            var r = numerator / denominator;
            if (r < 0 || r > 1)
            {
                return new IntersectionResult();
            }

            var s = ((deltaACy * deltaBAx) - (deltaACx * deltaBAy)) / denominator;
            if (s < 0 || s > 1)
            {
                return new IntersectionResult();
            }

            return new IntersectionResult(true, new Vector2((float)(lineSegment1Start.X + (r * deltaBAx)), (float)(lineSegment1Start.Y + (r * deltaBAy))));
        }

        // Vector2 class extended methods:

        /// <summary>
        ///     Returns true if the vector is valid.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns></returns>
        public static bool IsValid(this Vector2 v)
        {
            return v != Vector2.Zero;
        }

        /// <summary>
        ///     Determines whether this instance is valid.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns></returns>
        public static bool IsValid(this Vector3 v)
        {
            return v != Vector3.Zero;
        }

        /// <summary>
        ///     Joins all the polygones in the list in one polygone if they interect.
        /// </summary>
        /// <param name="sList">The polygon list.</param>
        /// <returns></returns>
        public static List<Polygon> JoinPolygons(this List<Polygon> sList)
        {
            var p = ClipPolygons(sList);
            var tList = new List<List<IntPoint>>();

            var c = new Clipper();
            c.AddPaths(p, PolyType.ptClip, true);
            c.Execute(ClipType.ctUnion, tList, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            return ToPolygons(tList);
        }

        /// <summary>
        ///     Joins all the polygones.
        ///     ClipType: http://www.angusj.com/delphi/clipper/documentation/Docs/Units/ClipperLib/Types/ClipType.htm
        ///     PolyFillType: http://www.angusj.com/delphi/clipper/documentation/Docs/Units/ClipperLib/Types/PolyFillType.htm
        /// </summary>
        /// <param name="sList">The s list.</param>
        /// <param name="cType">Type of the c.</param>
        /// <param name="pType">Type of the p.</param>
        /// <param name="pFType1">The p f type1.</param>
        /// <param name="pFType2">The p f type2.</param>
        /// <returns></returns>
        public static List<Polygon> JoinPolygons(
            this List<Polygon> sList,
            ClipType cType,
            PolyType pType = PolyType.ptClip,
            PolyFillType pFType1 = PolyFillType.pftNonZero,
            PolyFillType pFType2 = PolyFillType.pftNonZero)
        {
            var p = ClipPolygons(sList);
            var tList = new List<List<IntPoint>>();

            var c = new Clipper();
            c.AddPaths(p, pType, true);
            c.Execute(cType, tList, pFType1, pFType2);

            return ToPolygons(tList);
        }

        /// <summary>
        ///     Moves the polygone to the set position. It dosent rotate the polygone.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="moveTo">The move to.</param>
        /// <returns></returns>
        public static Polygon MovePolygone(this Polygon polygon, Vector2 moveTo)
        {
            var p = new Polygon();

            p.Add(moveTo);

            var count = polygon.Points.Count;

            var startPoint = polygon.Points[0];

            for (var i = 1; i < count; i++)
            {
                var polygonePoint = polygon.Points[i];

                p.Add(new Vector2(moveTo.X + (polygonePoint.X - startPoint.X), moveTo.Y + (polygonePoint.Y - startPoint.Y)));
            }

            return p;
        }

        /// <summary>
        ///     Returns the vector normalized.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns></returns>
        public static Vector2 Normalized(this Vector2 v)
        {
            v.Normalize();
            return v;
        }

        /// <summary>
        ///     Normalizes the specified vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns></returns>
        // public static Vector3 Normalized(this Vector3 v)
        // {
        // v.Normalize();
        // return v;
        // }
        /// <summary>
        ///     Returns the total distance of a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static float PathLength(this List<Vector2> path)
        {
            var distance = 0f;
            for (var i = 0; i < (path.Count - 1); i++)
            {
                distance += path[i].Distance2D(path[i + 1]);
            }

            return distance;
        }

        /// <summary>
        ///     Returns the perpendicular vector.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static Vector2 Perpendicular(this Vector2 v)
        {
            return new Vector2(-v.Y, v.X);
        }

        /// <summary>
        ///     Returns the second perpendicular vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns></returns>
        public static Vector2 Perpendicular2(this Vector2 v)
        {
            return new Vector2(v.Y, -v.X);
        }

        /// <summary>
        ///     Returns the polar for vector angle (in Degrees).
        /// </summary>
        /// <param name="v1">The vector.</param>
        /// <returns></returns>
        public static float Polar(this Vector2 v1)
        {
            if (Close(v1.X, 0, 0))
            {
                if (v1.Y > 0)
                {
                    return 90;
                }

                return v1.Y < 0 ? 270 : 0;
            }

            var theta = RadianToDegree(Math.Atan(v1.Y / v1.X));
            if (v1.X < 0)
            {
                theta = theta + 180;
            }

            if (theta < 0)
            {
                theta = theta + 360;
            }

            return theta;
        }

        /// <summary>
        ///     Returns the position where the vector will be after t(time) with s(speed) and delay.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="t">The time.</param>
        /// <param name="s">The speed.</param>
        /// <param name="delay">The delay.</param>
        /// <returns></returns>
        public static Vector2 PositionAfter(this List<Vector2> self, int t, int s, int delay = 0)
        {
            var distance = (Math.Max(0, t - delay) * s) / 1000;
            for (var i = 0; i <= (self.Count - 2); i++)
            {
                var from = self[i];
                var to = self[i + 1];
                var d = (int)to.Distance2D(from);
                if (d > distance)
                {
                    return from + (distance * (to - @from).Normalized());
                }

                distance -= d;
            }

            return self[self.Count - 1];
        }

        /// <summary>
        ///     Returns the projection of the Vector2 on the segment.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="segmentStart">The segment start.</param>
        /// <param name="segmentEnd">The segment end.</param>
        /// <returns></returns>
        public static ProjectionInfo ProjectOn(this Vector2 point, Vector2 segmentStart, Vector2 segmentEnd)
        {
            var cx = point.X;
            var cy = point.Y;
            var ax = segmentStart.X;
            var ay = segmentStart.Y;
            var bx = segmentEnd.X;
            var by = segmentEnd.Y;
            var rL = (((cx - ax) * (bx - ax)) + ((cy - ay) * (@by - ay))) / ((float)Math.Pow(bx - ax, 2) + (float)Math.Pow(by - ay, 2));
            var pointLine = new Vector2(ax + (rL * (bx - ax)), ay + (rL * (@by - ay)));
            float rS;
            if (rL < 0)
            {
                rS = 0;
            }
            else if (rL > 1)
            {
                rS = 1;
            }
            else
            {
                rS = rL;
            }

            var isOnSegment = rS.CompareTo(rL) == 0;
            var pointSegment = isOnSegment ? pointLine : new Vector2(ax + (rS * (bx - ax)), ay + (rS * (@by - ay)));
            return new ProjectionInfo(isOnSegment, pointSegment, pointLine);
        }

        /// <summary>
        ///     Converts radians to degrees.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static float RadianToDegree(double angle)
        {
            return (float)(angle * (180.0 / Math.PI));
        }

        /// <summary>
        ///     Rotates the vector around the set position.
        ///     Angle is in radians.
        /// </summary>
        /// <param name="rotated">The rotated.</param>
        /// <param name="around">The around.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static Vector2 RotateAroundPoint(this Vector2 rotated, Vector2 around, float angle)
        {
            var sin = Math.Sin(angle);
            var cos = Math.Cos(angle);

            var x = ((cos * (rotated.X - around.X)) - (sin * (rotated.Y - around.Y))) + around.X;
            var y = (sin * (rotated.X - around.X)) + (cos * (rotated.Y - around.Y)) + around.Y;

            return new Vector2((float)x, (float)y);
        }

        /// <summary>
        ///     Rotates the vector a set angle (angle in radians).
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static Vector2 Rotated(this Vector2 v, float angle)
        {
            var c = Math.Cos(angle);
            var s = Math.Sin(angle);

            return new Vector2((float)((v.X * c) - (v.Y * s)), (float)((v.Y * c) + (v.X * s)));
        }

        /// <summary>
        ///     Rotates the polygon around the set position.
        ///     Angle is in radians.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="around">The around.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        public static Polygon RotatePolygon(this Polygon polygon, Vector2 around, float angle)
        {
            var p = new Polygon();

            foreach (var polygonePoint in polygon.Points.Select(poinit => RotateAroundPoint(poinit, around, angle)))
            {
                p.Add(polygonePoint);
            }

            return p;
        }

        /// <summary>
        ///     Rotates the polygon around to the set direction.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="around">The around.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static Polygon RotatePolygon(this Polygon polygon, Vector2 around, Vector2 direction)
        {
            var deltaX = around.X - direction.X;
            var deltaY = around.Y - direction.Y;
            var angle = (float)Math.Atan2(deltaY, deltaX);
            return RotatePolygon(polygon, around, angle - DegreeToRadian(90));
        }

        /// <summary>
        ///     Shortens the specified vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <param name="to">The vector to shorten from.</param>
        /// <param name="distance">The distance.</param>
        /// <returns></returns>
        public static Vector2 Shorten(this Vector2 v, Vector2 to, float distance)
        {
            return v - (distance * (to - v).Normalized());
        }

        /// <summary>
        ///     Shortens the specified vector.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <param name="to">The vector to shorten from.</param>
        /// <param name="distance">The distance.</param>
        /// <returns></returns>
        public static Vector3 Shorten(this Vector3 v, Vector3 to, float distance)
        {
            return v - (distance * (to - v).Normalized());
        }

        /// <summary>
        ///     Switches the Y and Z.
        /// </summary>
        /// <param name="v">The vector.</param>
        /// <returns></returns>
        public static Vector3 SwitchYZ(this Vector3 v)
        {
            return new Vector3(v.X, v.Z, v.Y);
        }

        // Vector3 class extended methods:

        /// <summary>
        ///     Converts a Vector3 to Vector2
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static Vector2 To2D(this Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        /// <summary>
        ///     Converts a 3D path to 2D
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static List<Vector2> To2D(this List<Vector3> path)
        {
            return path.Select(point => point.To2D()).ToList();
        }

        /// <summary>
        ///     Converts a list of <see cref="IntPoint" /> to a polygon.
        /// </summary>
        /// <param name="v">The int points.</param>
        /// <returns></returns>
        public static Polygon ToPolygon(this List<IntPoint> v)
        {
            var polygon = new Polygon();
            foreach (var point in v)
            {
                polygon.Add(new Vector2(point.X, point.Y));
            }

            return polygon;
        }

        /// <summary>
        ///     Converts a list of list points to a polygon.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static List<Polygon> ToPolygons(this List<List<IntPoint>> v)
        {
            return v.Select(path => path.ToPolygon()).ToList();
        }

        /// <summary>
        ///     Gets the vectors movement collision.
        /// </summary>
        /// <param name="startPoint1">The start point1.</param>
        /// <param name="endPoint1">The end point1.</param>
        /// <param name="v1">The v1.</param>
        /// <param name="startPoint2">The start point2.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="delay">The delay.</param>
        /// <returns></returns>
        public static Tuple<float, Vector2> VectorMovementCollision(Vector2 startPoint1, Vector2 endPoint1, float v1, Vector2 startPoint2, float v2, float delay = 0f)
        {
            float sP1x = startPoint1.X, sP1y = startPoint1.Y, eP1x = endPoint1.X, eP1y = endPoint1.Y, sP2x = startPoint2.X, sP2y = startPoint2.Y;

            float d = eP1x - sP1x, e = eP1y - sP1y;
            float dist = (float)Math.Sqrt((d * d) + (e * e)), t1 = float.NaN;
            float S = Math.Abs(dist) > float.Epsilon ? (v1 * d) / dist : 0, K = Math.Abs(dist) > float.Epsilon ? (v1 * e) / dist : 0f;

            float r = sP2x - sP1x, j = sP2y - sP1y;
            var c = (r * r) + (j * j);

            if (dist > 0f)
            {
                if (Math.Abs(v1 - float.MaxValue) < float.Epsilon)
                {
                    var t = dist / v1;
                    t1 = (v2 * t) >= 0f ? t : float.NaN;
                }
                else if (Math.Abs(v2 - float.MaxValue) < float.Epsilon)
                {
                    t1 = 0f;
                }
                else
                {
                    float a = ((S * S) + (K * K)) - (v2 * v2), b = (-r * S) - (j * K);

                    if (Math.Abs(a) < float.Epsilon)
                    {
                        if (Math.Abs(b) < float.Epsilon)
                        {
                            t1 = Math.Abs(c) < float.Epsilon ? 0f : float.NaN;
                        }
                        else
                        {
                            var t = -c / (2 * b);
                            t1 = (v2 * t) >= 0f ? t : float.NaN;
                        }
                    }
                    else
                    {
                        var sqr = (b * b) - (a * c);
                        if (sqr >= 0)
                        {
                            var nom = (float)Math.Sqrt(sqr);
                            var t = (-nom - b) / a;
                            t1 = (v2 * t) >= 0f ? t : float.NaN;
                            t = (nom - b) / a;
                            var t2 = (v2 * t) >= 0f ? t : float.NaN;

                            if (!float.IsNaN(t2) && !float.IsNaN(t1))
                            {
                                if (t1 >= delay && t2 >= delay)
                                {
                                    t1 = Math.Min(t1, t2);
                                }
                                else if (t2 >= delay)
                                {
                                    t1 = t2;
                                }
                            }
                        }
                    }
                }
            }
            else if (Math.Abs(dist) < float.Epsilon)
            {
                t1 = 0f;
            }

            return new Tuple<float, Vector2>(t1, !float.IsNaN(t1) ? new Vector2(sP1x + (S * t1), sP1y + (K * t1)) : Vector2.Zero);
        }
    }
}