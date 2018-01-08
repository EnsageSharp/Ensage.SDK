// <copyright file="HotkeySelectorView.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;

    using SharpDX;
    using SharpDX.Direct3D9;

    [ExportView(typeof(HotkeySelector))]
    public class HotkeySelectorView : View
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

            var rightSide = (context.Position.X + size.X) - border.Thickness[2] - 10;

            var propValue = item.ValueBinding.GetValue<HotkeySelector>();
            pos.X = rightSide - item.ValueSize.X;
            context.Renderer.DrawTexture(activeStyle.Menu, new RectangleF(pos.X - 10, pos.Y, item.ValueSize.X + 20, item.ValueSize.Y));

            pos.X = rightSide - item.ValueSize.X - styleConfig.TextSpacing;

            var rect = new RectangleF();
            rect.X = pos.X;
            rect.Y = pos.Y;
            rect.Right = rightSide;
            rect.Bottom = context.Position.Y + size.Y;

            context.Renderer.DrawText(rect, propValue.ToString(), styleConfig.Font.Color, FontDrawFlags.Center, font.Size, font.Family);
        }

        public override Vector2 GetSize(MenuBase context)
        {
            var totalSize = base.GetSize(context);
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var item = (MenuItemEntry)context;
            var font = styleConfig.Font;

            // get size of longest possible hotkey
            item.ValueSize = context.Renderer.MessureText(MouseButtons.XButton2Down.ToString(), font.Size, font.Family);
            totalSize.X += styleConfig.TextSpacing + item.ValueSize.X + 20;

            return totalSize;
        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
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

                var rectPos = new RectangleF();
                rectPos.Left = (context.Position.X + size.X) - border.Thickness[2] - item.ValueSize.X - styleConfig.TextSpacing - 20;
                rectPos.Top = pos.Y + border.Thickness[1];
                rectPos.Width = item.ValueSize.X + 20;
                rectPos.Height = item.ValueSize.Y;
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