// <copyright file="IRenderManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;

    using SharpDX;

    using Color = System.Drawing.Color;

    public interface IRenderManager : IDisposable
    {
        event RenderManager.EventHandler Draw;

        ITextureManager TextureManager { get; }

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawCircle(Vector2 center, float radius, Color color, float width = 1.0f);

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1.0f);

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawFilledRectangle(RectangleF rect, Color color);

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f);

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1.0f);

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri");

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri");

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0.0f, float opacity = 1.0f);

        [Obsolete("Use renderer from OnDraw callback")]
        void DrawTexture(string textureKey, RectangleF rect, float rotation = 0.0f, float opacity = 1.0f);

        Vector2 GetTextureSize(string textureKey);

        Vector2 MeasureText(string text, float fontSize = 13f, string fontFamily = "Calibri");
    }
}