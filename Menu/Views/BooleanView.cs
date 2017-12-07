// <copyright file="BooleanView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;

    using SharpDX;

    using Color = System.Drawing.Color;

    [ExportView(typeof(bool))]
    public class BooleanView : IView
    {
        public void Draw(MenuBase context)
        {
            var item = (MenuItemEntry)context;

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;

            context.Renderer.DrawTexture(activeStyle.Item, new RectangleF(pos.X, pos.Y, size.X, size.Y));

            var font = styleConfig.Font;
            context.Renderer.DrawText(pos + new Vector2(border.Thickness[0], border.Thickness[1]), context.Name, styleConfig.Font.Color, font.Size, font.Family);

            var sliderPos = Vector2.Zero;
            sliderPos.X = pos.X + size.X - border.Thickness[2] - (styleConfig.ArrowSize.X * 2);
            sliderPos.Y = pos.Y + border.Thickness[1];
            context.Renderer.DrawTexture(
                activeStyle.Slider,
                new RectangleF(sliderPos.X, sliderPos.Y, styleConfig.ArrowSize.X * 2, styleConfig.ArrowSize.Y));

            if (item.PropertyBinding.GetValue<bool>())
            {
                sliderPos.X += styleConfig.ArrowSize.X;
                context.Renderer.DrawTexture(activeStyle.Checked, new RectangleF(sliderPos.X, sliderPos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
            }
            else
            {
                context.Renderer.DrawTexture(activeStyle.Unchecked, new RectangleF(sliderPos.X, sliderPos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
            }
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
            totalSize.X += styleConfig.LineWidth + fontSize.X + styleConfig.TextSpacing + (styleConfig.ArrowSize.X * 2);
            totalSize.Y += Math.Max(fontSize.Y, styleConfig.ArrowSize.Y);
            return totalSize;
        }

        public void OnClick(MenuBase context, Vector2 clickPosition)
        {
            if (context is MenuItemEntry item)
            {
                item.Value = !item.PropertyBinding.GetValue<bool>();
            }
        }
    }
}