// <copyright file="LineAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public abstract class LineAbility : PointAbility
    {
        protected LineAbility(Ability ability)
            : base(ability)
        {
        }

        public abstract float Width { get; }

        public override float GetDamage(params Unit[] targets)
        {
            var level = this.Ability.Level;
            if (level == 0)
            {
                return 0;
            }

            var damage = this.Ability.GetDamage(level - 1);

            var target = targets.First();
            if (!this.CanAffectTarget(target))
            {
                return 0;
            }

            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target);

            return damage * (1.0f + amplify) * (1.0f - reduction);
        }

        public override Polygon GetPolygon(Vector3 position)
        {
            var start = this.Ability.Owner.NetworkPosition;
            var dir = (position - start).Normalized();
            var end = start + dir * this.Range;

            return new Polygon.Rectangle(start, end, this.Width);
        }

        public override Polygon GetPolygon(Vector3 position, float time)
        {
            var range = this.Range * time * this.Speed;

            var start = this.Ability.Owner.NetworkPosition;
            var dir = (position - start).Normalized();
            var end = start + dir * range;

            return new Polygon.Rectangle(start, end, this.Width);
        }
    }
}