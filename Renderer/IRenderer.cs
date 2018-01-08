// <copyright file="IRenderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;

    using SharpDX;
    using SharpDX.Direct3D9;
    using SharpDX.DirectWrite;

    using Color = System.Drawing.Color;

    public interface IRenderer : IDisposable
    {
        event EventHandler Draw;

        ITextureManager TextureManager { get; }

        void DrawCircle(Vector2 center, float radius, Color color, float width = 1.0f);

        void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f);

        void DrawRectangle(RectangleF rect, Color color, float width = 1.0f);

        void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri");

        void DrawText(RectangleF position, string text, Color color, FontDrawFlags flags = FontDrawFlags.Left, float fontSize = 13f, string fontFamily = "Calibri");

        void DrawTexture(string textureKey, RectangleF rect, float rotation = 0.0f, float opacity = 1.0f);

        Vector2 MessureText(string text, float fontSize = 13f, string fontFamily = "Calibri");

        Vector2 GetTextureSize(string textureKey);
    }
}