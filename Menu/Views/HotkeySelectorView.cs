// <copyright file="HotkeyPressSelectorView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [ExportView(typeof(HotkeySelector))]
    public class HotkeySelectorView : IView
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
            context.Renderer.DrawText(pos, context.Name, styleConfig.Font.Color, font.Size, font.Family);

            var propValue = item.ValueBinding.GetValue<HotkeySelector>();
            var textSize = context.Renderer.MessureText(propValue.ToString(), font.Size, font.Family);
            pos.X = (context.Position.X + size.X) - border.Thickness[2] - textSize.X - styleConfig.TextSpacing - 10;
            context.Renderer.DrawTexture(activeStyle.Menu, new RectangleF(pos.X - 10, pos.Y, textSize.X + 20, textSize.Y));
            context.Renderer.DrawText(pos, propValue.ToString(), styleConfig.Font.Color, font.Size, font.Family);
        }

        public Vector2 GetSize(MenuBase context)
        {
            var totalSize = Vector2.Zero;
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var border = styleConfig.Border;
            totalSize.X += border.Thickness[0] + border.Thickness[2];
            totalSize.Y += border.Thickness[1] + border.Thickness[3];

            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<HotkeySelector>();

            var font = styleConfig.Font;
            var textSize = context.Renderer.MessureText(context.Name, font.Size, font.Family);
            totalSize.X += styleConfig.TextSpacing + textSize.X;
            totalSize.Y += Math.Max(textSize.Y, styleConfig.ArrowSize.Y);

            textSize = context.Renderer.MessureText(propValue.ToString(), font.Size, font.Family);
            totalSize.X += styleConfig.TextSpacing + textSize.X + 20;

            return totalSize;
        }

        public bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            if ((buttons & MouseButtons.LeftDown) == MouseButtons.LeftDown)
            {
                var pos = context.Position;
                var size = context.RenderSize;

                var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;
                var font = styleConfig.Font;
                var border = styleConfig.Border;

                var item = (MenuItemEntry)context;
                var propValue = item.ValueBinding.GetValue<HotkeySelector>();
                var textSize = context.Renderer.MessureText(propValue.ToString(), font.Size, font.Family);

                var rectPos = new RectangleF();
                rectPos.Left = (context.Position.X + size.X) - border.Thickness[2] - textSize.X - styleConfig.TextSpacing - 20;
                rectPos.Top = pos.Y + border.Thickness[1];
                rectPos.Width = textSize.X + 20;
                rectPos.Height = textSize.Y;
                if (rectPos.Contains(clickPosition))
                {
                    propValue.AssignNewHotkey();
                    return true;
                }
            }

            return false;
        }
    }
}