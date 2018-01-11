// <copyright file="BooleanView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;

    using SharpDX;

    [ExportView(typeof(bool))]
    public class BooleanView : View
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

            var sliderPos = Vector2.Zero;
            sliderPos.X = (pos.X + size.X) - border.Thickness[2] - (styleConfig.ArrowSize.X * 2);
            sliderPos.Y = pos.Y + border.Thickness[1];
            context.Renderer.DrawTexture(activeStyle.Slider, new RectangleF(sliderPos.X, sliderPos.Y, styleConfig.ArrowSize.X * 2, styleConfig.ArrowSize.Y));

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
            context.Renderer.DrawText(pos, context.Name, font.Color, font.Size, font.Family);

            if (item.ValueBinding.GetValue<bool>())
            {
                sliderPos.X += styleConfig.ArrowSize.X;
                context.Renderer.DrawTexture(activeStyle.Checked, new RectangleF(sliderPos.X, sliderPos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
            }
            else
            {
                context.Renderer.DrawTexture(activeStyle.Unchecked, new RectangleF(sliderPos.X, sliderPos.Y, styleConfig.ArrowSize.X, styleConfig.ArrowSize.Y));
            }
        }

        public override Vector2 GetSize(MenuBase context)
        {
            var totalSize = base.GetSize(context);
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;
            totalSize.X += styleConfig.TextSpacing + (styleConfig.ArrowSize.X * 2); // TODO: use own bool view style options
            totalSize.Y = Math.Max(totalSize.Y, styleConfig.ArrowSize.Y); // border missing for y

            if (!string.IsNullOrEmpty(context.TextureKey))
            {
                var textureSize = context.Renderer.GetTextureSize(context.TextureKey);
                var scale = totalSize.Y / textureSize.Y;
                totalSize.X += styleConfig.TextSpacing + (textureSize.X * scale);
            }
            
            return totalSize;
        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            if ((buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp && context is MenuItemEntry item)
            {
                item.Value = !item.ValueBinding.GetValue<bool>();
                return true;
            }

            return false;
        }
    }
}
