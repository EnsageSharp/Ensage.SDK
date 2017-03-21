// <copyright file="Direct2D11Renderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer
{
    using System;

    using global::Ensage.SDK.Geometry;

    using SharpDX;

    public class Direct2D11Renderer : IDirect2DRenderer
    {
        public IBrushManager Brushes { get; }

        public IRenderContext Context { get; }

        public IFontManager Fonts { get; }

        public IRenderObjectManager Objects { get; }

        public void DrawLine(Vector2 start, Vector2 end)
        {
            throw new NotImplementedException();
        }

        public void DrawPolygon(Geometry.Polygon polygon)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Vector2 start, Vector2 end)
        {
            throw new NotImplementedException();
        }

        public void DrawText(string text, Vector2 position)
        {
            throw new NotImplementedException();
        }
    }
}