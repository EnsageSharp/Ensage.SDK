// <copyright file="ConeAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using SharpDX;

    public abstract class ConeAbility : PointAbility
    {
        protected ConeAbility(Ability ability)
            : base(ability)
        {
        }

        public abstract float EndWidth { get; }

        public abstract float StartWidth { get; }

        public override Geometry.Polygon GetPolygon(Vector3 position)
        {
            var start = this.Ability.Owner.NetworkPosition;
            var dir = (position - start).Normalized();
            var end = start + dir * this.Range;

            return new Geometry.Polygon.Cone(start, end, this.StartWidth, this.EndWidth);
        }

        public override Geometry.Polygon GetPolygon(Vector3 position, float time)
        {
            var range = this.Range * time * this.Speed;

            var startRadius = this.StartWidth;
            var endRadius = this.EndWidth;

            var start = this.Ability.Owner.NetworkPosition;
            var dir = (position - start).Normalized();
            var end = start + dir * range;

            endRadius = startRadius + (endRadius - startRadius) / this.GetTravelTime() * time;

            return new Geometry.Polygon.Cone(start, end, startRadius, endRadius);
        }
    }
}