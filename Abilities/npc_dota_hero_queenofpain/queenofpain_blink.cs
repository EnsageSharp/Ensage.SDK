// <copyright file="queenofpain_blink.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using Ensage.SDK.Geometry;

    using SharpDX;

    public class queenofpain_blink : PointAbility
    {
        public queenofpain_blink(Ability ability)
            : base(ability)
        {
        }

        public override float GetDamage(params Unit[] target)
        {
            return 0.0f;
        }

        public override Polygon GetPolygon(Vector3 position)
        {
            var unit = this.Ability.Owner as Unit;
            var radius = unit?.HullRadius ?? 16; // TODO: DOTA_HULL_SIZE_REGULAR 
            return new Polygon.Circle(position, radius);
        }
    }
}