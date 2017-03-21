// <copyright file="ID2DRenderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using Ensage.SDK.Geometry;

    using SharpDX.Direct2D1;
    using SharpDX.DirectWrite;

    public interface ID2DRenderer
    {
        ID2DBrushContainer Brushes { get; }

        ID2DContext Context { get; }

        ID2DFontContainer Fonts { get; }

        void DrawBox2D(
            float x,
            float y,
            float width,
            float height,
            float stroke,
            SolidColorBrush brush,
            SolidColorBrush floateriorBrush);

        void DrawBox3D(
            float x,
            float y,
            float width,
            float height,
            float length,
            float stroke,
            SolidColorBrush brush,
            SolidColorBrush floateriorBrush);

        void DrawCircle(float x, float y, float radius, float stroke, SolidColorBrush brush);

        void DrawLine(float startX, float startY, float endX, float endY, float stroke, SolidColorBrush brush);

        void DrawPolygon(Polygon polygon);

        void DrawRectangle(float x, float y, float width, float height, float stroke, SolidColorBrush brush);

        void DrawRectangle3D(
            float x,
            float y,
            float width,
            float height,
            float length,
            float stroke,
            SolidColorBrush brush);

        void DrawText(float x, float y, string text, TextFormat font, SolidColorBrush brush);

        void FillCircle(float x, float y, float radius, SolidColorBrush brush);

        void FillRectangle(float x, float y, float width, float height, SolidColorBrush brush);
    }
}