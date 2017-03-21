// <copyright file="IDirect2DRenderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer
{
    using global::Ensage.SDK.Geometry;

    using SharpDX;

    public interface IDirect2DRenderer
    {
        IBrushManager Brushes { get; }

        IRenderContext Context { get; }

        IFontManager Fonts { get; }

        IRenderObjectManager Objects { get; }

        void DrawLine(Vector2 start, Vector2 end);

        void DrawPolygon(Geometry.Polygon polygon);

        void DrawRectangle(Vector2 start, Vector2 end);

        void DrawText(string text, Vector2 position);
    }
}