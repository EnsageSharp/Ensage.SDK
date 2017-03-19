// <copyright file="PointAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using SharpDX;

    public abstract class PointAbility : RangedAbility, IPolygonAbility
    {
        protected PointAbility(Ability ability)
            : base(ability)
        {
        }

        public abstract Geometry.Polygon GetPolygon(Vector3 position);

        public virtual Geometry.Polygon GetPolygon(Vector3 position, float time)
        {
            return this.GetPolygon(position);
        }
    }
}