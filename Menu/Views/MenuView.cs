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
    public class MenuView : View
    {
        public override void Draw(MenuBase context)
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

            pos.X = (context.Position.X + size.X) - border.Thickness[2] - styleConfig.ArrowSize.X - styleConfig.TextSpacing;
            if (context.IsHovered || !menuEntry.IsCollapsed)
            {
                pos.X += styleConfig.TextSpacing;
            }

            context.Renderer.DrawTexture(activeStyle.ArrowRight, new RectangleF(pos.X, pos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
        }

        public override Vector2 GetSize(MenuBase context)
        {
            var totalSize = base.GetSize(context);

            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;
            totalSize.X += styleConfig.TextSpacing + styleConfig.ArrowSize.X + (styleConfig.TextSpacing * 2);

            if (!string.IsNullOrEmpty(context.TextureKey))
            {
                var textureSize = context.Renderer.GetTextureSize(context.TextureKey);

                totalSize.Y = Math.Max(totalSize.Y, styleConfig.ArrowSize.Y);

                var scale = totalSize.Y / textureSize.Y;
                totalSize.X += styleConfig.TextSpacing + (textureSize.X * scale);
            }

            return totalSize;

        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
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