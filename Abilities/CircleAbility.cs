// <copyright file="CircleAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;

    using SharpDX;

    public abstract class CircleAbility : PointAbility
    {
        protected CircleAbility(Ability ability)
            : base(ability)
        {
        }

        public virtual float Radius => this.Ability.GetAbilitySpecialData("radius");

        public override Polygon GetPolygon(Vector3 position)
        {
            return new Polygon.Circle(position, this.Radius);
        }
    }
}