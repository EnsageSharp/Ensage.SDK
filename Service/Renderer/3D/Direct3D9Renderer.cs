// <copyright file="Direct3D9Renderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer
{
    using System;

    using global::Ensage.SDK.Geometry;

    using SharpDX;

    public class Direct3D9Renderer : IDirect3DRenderer
    {
        public void DrawLine(Vector3 start, Vector3 end)
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon(Geometry.Polygon polygon)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Vector3 start, Vector3 end)
        {
            throw new NotImplementedException();
        }

        public void DrawText(string text, Vector3 position)
        {
            throw new NotImplementedException();
        }
    }
}