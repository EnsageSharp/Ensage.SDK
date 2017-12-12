// <copyright file="StringView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;

    using SharpDX;

    [ExportView(typeof(string))]
    public class StringView : IView
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

            pos += new Vector2(border.Thickness[0], border.Thickness[1]);

            var font = styleConfig.Font;
            context.Renderer.DrawText(pos, context.Name, font.Color, font.Size, font.Family);

            var textSize = context.Renderer.MessureText(item.PropertyBinding.GetValue<string>(), font.Size, font.Family);
            pos.X = (context.Position.X + size.X) - border.Thickness[2] - styleConfig.TextSpacing - textSize.X;
            context.Renderer.DrawText(pos, item.PropertyBinding.GetValue<string>(), font.Color, font.Size, font.Family);
        }

        public Vector2 GetSize(MenuBase context)
        {
            var totalSize = Vector2.Zero;
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var border = styleConfig.Border;
            totalSize.X += border.Thickness[0] + border.Thickness[2];
            totalSize.Y += border.Thickness[1] + border.Thickness[3];

            var font = styleConfig.Font;
            var textSize = context.Renderer.MessureText(context.Name, font.Size, font.Family);
            totalSize.X += styleConfig.LineWidth + textSize.X + styleConfig.ArrowSize.X + (styleConfig.TextSpacing * 3);
            totalSize.Y += Math.Max(textSize.Y, styleConfig.ArrowSize.Y);

            var item = (MenuItemEntry)context;
            textSize = context.Renderer.MessureText(item.PropertyBinding.GetValue<string>(), font.Size, font.Family);
            totalSize.X += textSize.X;

            return totalSize;
        }

        public bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            return false;
        }
    }
}