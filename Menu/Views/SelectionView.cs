// <copyright file="SelectionView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [ExportView(typeof(Selection<>))]
    public class SelectionView : IView
    {
        private Vector2 textSize = Vector2.Zero;

        private Dictionary<string, Vector2> valueSizes = new Dictionary<string, Vector2>();

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
            context.Renderer.DrawText(pos, context.Name, styleConfig.Font.Color, font.Size, font.Family);

            var propValue = item.ValueBinding.GetValue<ISelection<object>>();
            pos.X = (context.Position.X + size.X) - border.Thickness[2] - (2 * styleConfig.ArrowSize.X) - (2 * styleConfig.TextSpacing) - this.valueSizes[propValue.ToString()].X;

            context.Renderer.DrawTexture(activeStyle.ArrowLeft, new RectangleF(pos.X, pos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
            pos.X += styleConfig.ArrowSize.X + styleConfig.TextSpacing;

            context.Renderer.DrawText(pos, propValue.Value.ToString(), styleConfig.Font.Color, font.Size, font.Family);
            pos.X += this.valueSizes[propValue.ToString()].X + styleConfig.TextSpacing;
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
            this.textSize = context.Renderer.MessureText(context.Name, font.Size, font.Family);
            totalSize.X += styleConfig.LineWidth + this.textSize.X + styleConfig.TextSpacing + (styleConfig.ArrowSize.X * 2);
            totalSize.Y += Math.Max(this.textSize.Y, styleConfig.ArrowSize.Y);

            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<ISelection<object>>();

            var maxSize = Vector2.Zero;
            foreach (var value in propValue.Values)
            {
                var tmp = context.Renderer.MessureText(value.ToString(), font.Size, font.Family);
                this.valueSizes[value.ToString()] = tmp;
                if (tmp.LengthSquared() > maxSize.LengthSquared())
                {
                    maxSize = tmp;
                }
            }

            totalSize.X += maxSize.X;

            return totalSize;
        }

        public bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            if ((buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                var size = context.RenderSize;

                var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

                var item = (MenuItemEntry)context;
                var propValue = item.ValueBinding.GetValue<ISelection<object>>();
                var textSize = this.valueSizes[propValue.ToString()].X;

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