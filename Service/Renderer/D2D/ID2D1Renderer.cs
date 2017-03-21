// <copyright file="ID2D1Renderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using global::Ensage.SDK.Geometry;

    using SharpDX.Direct2D1;
    using SharpDX.DirectWrite;

    public interface ID2D1Renderer
    {
        ID2D1BrushContainer Brushes { get; }

        ID2D1Context Context { get; }

        ID2D1FontContainer Fonts { get; }

        void DrawBox2D(
            int x,
            int y,
            int width,
            int height,
            float stroke,
            SolidColorBrush brush,
            SolidColorBrush interiorBrush);

        void DrawBox3D(
            int x,
            int y,
            int width,
            int height,
            int length,
            float stroke,
            SolidColorBrush brush,
            SolidColorBrush interiorBrush);

        void DrawCircle(int x, int y, int radius, float stroke, SolidColorBrush brush);

        void DrawLine(int startX, int startY, int endX, int endY, float stroke, SolidColorBrush brush);

        void DrawPolygon(Polygon polygon);

        void DrawRectangle(int x, int y, int width, int height, float stroke, SolidColorBrush brush);

        void DrawRectangle3D(int x, int y, int width, int height, int length, float stroke, SolidColorBrush brush);

        void DrawText(int x, int y, string text, TextFormat font, SolidColorBrush brush);

        void FillCircle(int x, int y, int radius, SolidColorBrush brush);

        void FillRectangle(int x, int y, int width, int height, SolidColorBrush brush);
    }
}