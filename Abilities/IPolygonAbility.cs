// <copyright file="IPolygonAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Geometry;

    using SharpDX;

    public interface IPolygonAbility
    {
        Polygon GetPolygon(Vector3 position);

        Polygon GetPolygon(Vector3 position, float time);
    }
}