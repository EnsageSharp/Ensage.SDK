// <copyright file="PicturePickerView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    [ExportView(typeof(PicturePicker))]
    public class PicturePickerView : IView
    {
        public virtual void Draw(MenuBase context)
        {
            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<PicturePicker>();

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;

            context.Renderer.DrawTexture(activeStyle.Item, new RectangleF(pos.X, pos.Y, size.X, size.Y));

            pos += new Vector2(border.Thickness[0], border.Thickness[1]);

            var font = styleConfig.Font;
            context.Renderer.DrawText(pos, context.Name, font.Color, font.Size, font.Family);

            var picturePickerStyle = styleConfig.PicturePicker;
            pos.X = (context.Position.X + size.X) - border.Thickness[2] - picturePickerStyle.PictureSize.X - picturePickerStyle.LineWidth;
            foreach (var state in propValue.PictureStates.Reverse())
            {
                var rect = new RectangleF(pos.X, pos.Y, picturePickerStyle.PictureSize.X, picturePickerStyle.PictureSize.Y);
                context.Renderer.DrawTexture(state.Key, rect);
                context.Renderer.DrawRectangle(rect, state.Value ? picturePickerStyle.SelectedColor : picturePickerStyle.Color, picturePickerStyle.LineWidth);

                pos.X -= picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2);
            }
        }

        public virtual Vector2 GetSize(MenuBase context)
        {
            var totalSize = Vector2.Zero;
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var border = styleConfig.Border;
            totalSize.X += border.Thickness[0] + border.Thickness[2];
            totalSize.Y += border.Thickness[1] + border.Thickness[3];

            var font = styleConfig.Font;
            var fontSize = context.Renderer.MessureText(context.Name, font.Size, font.Family);
            totalSize.X += styleConfig.LineWidth + fontSize.X + styleConfig.ArrowSize.X + (styleConfig.TextSpacing * 3);
            totalSize.Y += Math.Max(Math.Max(fontSize.Y, styleConfig.ArrowSize.Y), styleConfig.PicturePicker.PictureSize.Y);

            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<PicturePicker>();
            totalSize.X += propValue.PictureStates.Count * styleConfig.PicturePicker.PictureSize.X;

            return totalSize;
        }

        public virtual bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            if ((buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                var state = this.GetItemUnderMouse(context, clickPosition);
                if (state.HasValue)
                {
                    var item = (MenuItemEntry)context;
                    var propValue = item.ValueBinding.GetValue<PicturePicker>();
                    propValue.PictureStates[state.Value.Key] = !state.Value.Value;
                    return true;
                }
            }

            return false;
        }

        protected virtual KeyValuePair<string, bool>? GetItemUnderMouse(MenuBase context, Vector2 mousePos)
        {
            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<PicturePicker>();

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;
            var picturePickerStyle = styleConfig.PicturePicker;

            pos.X += size.X - border.Thickness[2] - picturePickerStyle.PictureSize.X - picturePickerStyle.LineWidth;
            foreach (var state in propValue.PictureStates.Reverse())
            {
                var testRect = new RectangleF(pos.X, pos.Y, picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2), size.Y);
                if (testRect.Contains(mousePos))
                {
                    return state;
                }

                pos.X -= picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2);
            }

            return null;
        }
    }
}