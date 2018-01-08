// <copyright file="SelectionView.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;
    using System.Linq;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Renderer;

    using SharpDX;

    [ExportView(typeof(Selection<>))]
    public class SelectionView : View
    {
        public override void Draw(MenuBase context)
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
            context.Renderer.DrawText(pos, context.Name, styleConfig.Font.Color, font.Size, font.Family);

            var propValue = item.ValueBinding.GetValue<ISelection>();
            pos.X = (context.Position.X + size.X) - border.Thickness[2] - (2 * styleConfig.ArrowSize.X) - (2 * styleConfig.TextSpacing) - item.ValueSize.X;

            context.Renderer.DrawTexture(activeStyle.ArrowLeft, new RectangleF(pos.X, pos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
            pos.X += styleConfig.ArrowSize.X + styleConfig.TextSpacing;

            var rightSide = (context.Position.X + size.X) - border.Thickness[2] - 10;
            var rect = new RectangleF();
            rect.X = pos.X;
            rect.Y = pos.Y;
            rect.Width = item.ValueSize.X + styleConfig.TextSpacing;
            rect.Bottom = context.Position.Y + size.Y;
            context.Renderer.DrawText(rect, propValue.Value.ToString(), styleConfig.Font.Color, RendererFontFlags.Center, font.Size, font.Family);

            pos.X += item.ValueSize.X + styleConfig.TextSpacing;
            context.Renderer.DrawTexture(activeStyle.ArrowRight, new RectangleF(pos.X, pos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
        }

        public override Vector2 GetSize(MenuBase context)
        {
            var totalSize = base.GetSize(context);
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var font = styleConfig.Font;
            totalSize.X += styleConfig.TextSpacing + (styleConfig.ArrowSize.X * 2); // todo make selection style config
            totalSize.Y = Math.Max(totalSize.Y, styleConfig.ArrowSize.Y);

            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<ISelection>();

            var longestElement = propValue.Values.OrderByDescending(x => x.ToString().Length).First();
            item.ValueSize = context.Renderer.MessureText(longestElement.ToString(), font.Size, font.Family);

            totalSize.X += styleConfig.TextSpacing + item.ValueSize.X + styleConfig.TextSpacing;

            return totalSize;
        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            if ((buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                var size = context.RenderSize;
                var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

                var item = (MenuItemEntry)context;
                var propValue = item.ValueBinding.GetValue<ISelection>();
                var textSize = item.ValueSize.X;

                var rectPos = new RectangleF();
                rectPos.X = (context.Position.X + size.X) - styleConfig.Border.Thickness[2] - (2 * styleConfig.ArrowSize.X) - (2 * styleConfig.TextSpacing) - textSize;
                rectPos.Y = context.Position.Y;
                rectPos.Width = styleConfig.ArrowSize.X + (textSize / 2);
                rectPos.Height = size.Y;
                if (rectPos.Contains(clickPosition))
                {
                    propValue.DecrementSelectedIndex();
                    return true;
                }

                rectPos.X = (context.Position.X + size.X) - styleConfig.Border.Thickness[2] - styleConfig.ArrowSize.X - styleConfig.TextSpacing - (textSize / 2);
                if (rectPos.Contains(clickPosition))
                {
                    propValue.IncrementSelectedIndex();
                    return true;
                }
            }

            return false;
        }
    }
}