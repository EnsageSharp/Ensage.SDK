namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Geometry;

    using SharpDX;

    public interface IPolygonAbility
    {
        Geometry.Polygon GetPolygon(Vector3 position);

        Geometry.Polygon GetPolygon(Vector3 position, float time);
    }
}