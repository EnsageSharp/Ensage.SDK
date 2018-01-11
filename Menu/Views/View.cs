// <copyright file="View.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Renderer;

    using PlaySharp.Toolkit.Extensions;

    using SharpDX;

    using Color = System.Drawing.Color;

    public abstract class View
    {
        public abstract void Draw(MenuBase context);

        public virtual void DrawTooltip(MenuBase context)
        {
            if (!(context is MenuItemEntry item))
            {
                return;
            }

            if (!item.IsHovered || item.Tooltip.IsNullOrEmpty() || item.TooltipSize == Vector2.Zero)
            {
                return;
            }

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var style = activeStyle.StyleConfig.Tooltip;
            var border = style.Border;

            var pos = context.Position;
            pos.X += context.RenderSize.X;

            var rectangleF = new RectangleF(pos.X, pos.Y, item.TooltipSize.X, item.TooltipSize.Y);
            context.Renderer.DrawTexture(activeStyle.Menu, rectangleF);

            if (border.Color != Color.Transparent)
            {
                var p1 = new Vector2(rectangleF.X, rectangleF.Y);
                var p2 = new Vector2(rectangleF.Right, rectangleF.Y);
                var p3 = new Vector2(rectangleF.Right, rectangleF.Bottom);
                var p4 = new Vector2(rectangleF.X, rectangleF.Bottom);

                context.Renderer.DrawLine(p1, p2, border.Color, border.Thickness[1]);
                context.Renderer.DrawLine(p2, p3, border.Color, border.Thickness[2]);
                context.Renderer.DrawLine(p3, p4, border.Color, border.Thickness[3]);
                context.Renderer.DrawLine(p4, p1, border.Color, border.Thickness[0]);
            }

            var font = style.Font;
            context.Renderer.DrawText(rectangleF, item.Tooltip, font.Color, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, font.Size, font.Family);
        }

        public virtual Vector2 PermaDraw(PermaMenuItemEntry context, Vector2 position)
        {
            return position;
        }

        public virtual Vector2 GetPermaSize(PermaMenuItemEntry context)
        {
            var totalSize = Vector2.Zero;
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig.PermaShow;

            var border = styleConfig.Border;
            totalSize.X += border.Thickness[0] + border.Thickness[2];
            totalSize.Y += border.Thickness[1] + border.Thickness[3];

            var font = styleConfig.Font;
            context.PermaTextSize = context.Renderer.MessureText($"{context.RootMenuName} > {context.Name}", font.Size, font.Family);
            totalSize += context.PermaTextSize;

            return totalSize;
        }

        public virtual Vector2 GetSize(MenuBase context)
        {
            var totalSize = Vector2.Zero;
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var border = styleConfig.Border;
            totalSize.X += border.Thickness[0] + border.Thickness[2];
            totalSize.Y += border.Thickness[1] + border.Thickness[3];

            var font = styleConfig.Font;
            context.TextSize = context.Renderer.MessureText(context.Name, font.Size, font.Family);
            totalSize.X += styleConfig.LineWidth + context.TextSize.X;
            totalSize.Y += context.TextSize.Y;

            if (context is MenuItemEntry item)
            {
                var tooltipSize = Vector2.Zero;
                if (!item.Tooltip.IsNullOrEmpty())
                {
                    var style = styleConfig.Tooltip;

                    tooltipSize.X = style.Border.Thickness[0]
                                    + styleConfig.TextSpacing
                                    + context.Renderer.MessureText(item.Tooltip, style.Font.Size, style.Font.Family).X
                                    + styleConfig.TextSpacing
                                    + style.Border.Thickness[2];
                    tooltipSize.Y = totalSize.Y;
                }

                item.TooltipSize = tooltipSize;
            }

            return totalSize;
        }

        public abstract bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition);
    }
}