// <copyright file="StringView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;
    using System.Linq;

    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;

    using SharpDX;

    using Color = System.Drawing.Color;
    using Ensage.SDK.Menu.Items;

    [ExportView(typeof(PicturePicker))]
    public class PicturePickerView : IView
    {
 
        public void Draw(MenuBase context)
        {
            var item = (MenuItemEntry)context;
            var propValue = item.PropertyBinding.GetValue<PicturePicker>();

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;

            context.Renderer.DrawTexture(activeStyle.Item, new RectangleF(pos.X, pos.Y, size.X, size.Y));

            pos += new Vector2(border.Thickness[0], border.Thickness[1]);

            var font = styleConfig.Font;
            context.Renderer.DrawText(pos, context.Name, font.Color, font.Size, font.Family);

            var picturePickerStyle = styleConfig.PicturePickerStyle;
            pos.X = context.Position.X + size.X - border.Thickness[2] - picturePickerStyle.PictureSize.X - picturePickerStyle.LineWidth;
            foreach (var state in propValue.PictureStates.Reverse())
            {
                var rect = new RectangleF(pos.X, pos.Y, picturePickerStyle.PictureSize.X, picturePickerStyle.PictureSize.Y);
                context.Renderer.DrawTexture(state.Key, rect);
                context.Renderer.DrawRectangle(rect, state.Value ? picturePickerStyle.SelectedColor : picturePickerStyle.Color, picturePickerStyle.LineWidth);
                
                pos.X -= (picturePickerStyle.PictureSize.X + picturePickerStyle.LineWidth * 2);
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
            totalSize.X += styleConfig.LineWidth + fontSize.X + styleConfig.ArrowSize.X + (styleConfig.TextSpacing * 3);
            totalSize.Y += Math.Max(Math.Max(fontSize.Y, styleConfig.ArrowSize.Y), styleConfig.PicturePickerStyle.PictureSize.Y);

            var item = (MenuItemEntry)context;
            var propValue = item.PropertyBinding.GetValue<PicturePicker>();
            totalSize.X += propValue.PictureStates.Count * styleConfig.PicturePickerStyle.PictureSize.X;

            return totalSize;
        }

        public void OnClick(MenuBase context, Vector2 clickPosition)
        {
            var item = (MenuItemEntry)context;
            var propValue = item.PropertyBinding.GetValue<PicturePicker>();

            var pos = context.Position;
            var size = context.RenderSize;

            var activeStyle = context.MenuConfig.GeneralConfig.ActiveStyle.Value;
            var styleConfig = activeStyle.StyleConfig;
            var border = styleConfig.Border;
            var picturePickerStyle = styleConfig.PicturePickerStyle;

            pos.X += size.X - border.Thickness[2] - picturePickerStyle.PictureSize.X - picturePickerStyle.LineWidth;
            foreach (var state in propValue.PictureStates.Reverse())
            {
                var testRect = new RectangleF(pos.X, pos.Y, picturePickerStyle.PictureSize.X + (picturePickerStyle.LineWidth * 2), size.Y);
                if (testRect.Contains(clickPosition))
                {
                    propValue.PictureStates[state.Key] = !state.Value;
                    return;
                }

                pos.X -= (picturePickerStyle.PictureSize.X + picturePickerStyle.LineWidth * 2);
            }
           
        }
    }
}