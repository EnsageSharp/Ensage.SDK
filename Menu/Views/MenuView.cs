// <copyright file="MenuView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Entries;

    using SharpDX;

    [Export]
    public class MenuView : IView
    {
        public void Draw(MenuBase context)
        {
            var menuEntry = (MenuEntry)context;

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;

            context.Renderer.DrawTexture(activeStyle.Menu, new RectangleF(pos.X, pos.Y, size.X, size.Y));

            if (styleConfig.LineWidth > 0)
            {
                context.Renderer.DrawLine(pos, pos + new Vector2(0, size.Y), menuEntry.IsCollapsed ? styleConfig.LineColor : styleConfig.SelectedLineColor, styleConfig.LineWidth);
            }

            pos += new Vector2(border.Thickness[0] + styleConfig.LineWidth, border.Thickness[1]);
            if (!string.IsNullOrEmpty(context.TextureKey))
            {
                var textureSize = context.Renderer.GetTextureSize(context.TextureKey);
                var scale = (size.Y - border.Thickness[1] - border.Thickness[3]) / textureSize.Y;
                textureSize *= scale;
                context.Renderer.DrawTexture(context.TextureKey, new RectangleF(pos.X, pos.Y, textureSize.X, textureSize.Y));
                pos.X += textureSize.X + styleConfig.TextSpacing;
            }

            var font = styleConfig.Font;
            context.Renderer.DrawText(pos, context.Name, styleConfig.Font.Color, font.Size, font.Family);

            pos.X = (context.Position.X + size.X) - border.Thickness[2] - (styleConfig.TextSpacing * 2) - styleConfig.ArrowSize.X;
            if (context.IsHovered || !menuEntry.IsCollapsed)
            {
                pos.X += styleConfig.TextSpacing;
            }

            context.Renderer.DrawTexture(activeStyle.ArrowRight, new RectangleF(pos.X, pos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
        }

        public Vector2 GetSize(MenuBase context)
        {
            var totalSize = Vector2.Zero;
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var border = styleConfig.Border;
            totalSize.X += border.Thickness[0] + border.Thickness[2];
            totalSize.Y += border.Thickness[1] + border.Thickness[3];

            var font = styleConfig.Font;
            var fontSize = context.Renderer.MessureText(context.Name, font.Size, font.Family);
            totalSize.X += styleConfig.LineWidth + fontSize.X + styleConfig.ArrowSize.X + (styleConfig.TextSpacing * 3);

            var height = Math.Max(fontSize.Y, styleConfig.ArrowSize.Y);
            totalSize.Y += height;

            if (!string.IsNullOrEmpty(context.TextureKey))
            {
                var textureSize = context.Renderer.GetTextureSize(context.TextureKey);
                var scale = height / textureSize.Y;
                totalSize.X += (textureSize.X * scale) + styleConfig.TextSpacing;
            }

            return totalSize;
        }

        public bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            if ((buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                var entry = (MenuEntry)context;
                entry.IsCollapsed = !entry.IsCollapsed;
                return true;
            }

            return false;
        }
    }
}