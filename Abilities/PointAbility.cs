// <copyright file="PointAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Geometry;

    using SharpDX;

    public abstract class PointAbility : RangedAbility, IPolygonAbility
    {
        protected PointAbility(Ability ability)
            : base(ability)
        {
        }

        public abstract Polygon GetPolygon(Vector3 position);

        public virtual Polygon GetPolygon(Vector3 position, float time)
        {
            return this.GetPolygon(position);
        }
    }
}