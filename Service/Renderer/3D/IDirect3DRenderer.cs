// <copyright file="IDirect3DRenderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer
{
    using global::Ensage.SDK.Geometry;

    using SharpDX;

    public interface IDirect3DRenderer
    {
        void DrawLine(Vector3 start, Vector3 end);

        void DrawPolygon(Geometry.Polygon polygon);

        void DrawRectangle(Vector3 start, Vector3 end);

        void DrawText(string text, Vector3 position);
    }
}