// <copyright file="SliderView.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;
    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Renderer;

    using SharpDX;

    [ExportView(typeof(Slider<>))]
    public class SliderView : View
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

            var propValue = item.ValueBinding.GetValue<ISlider>();

            var rightSide = (context.Position.X + size.X) - border.Thickness[2];
            pos.X = rightSide - item.ValueSize.X - styleConfig.TextSpacing;

            var rect = new RectangleF();
            rect.X = pos.X;
            rect.Y = pos.Y;
            rect.Right = rightSide;
            rect.Bottom = context.Position.Y + size.Y;
            context.Renderer.DrawText(rect, propValue.ToString(), styleConfig.Font.Color, RendererFontFlags.Right, font.Size, font.Family);
            pos = context.Position;

            switch (propValue)
            {
                case ISlider<int> intSlider:
                    pos.X += ((float)(intSlider.Value - intSlider.MinValue) / (intSlider.MaxValue - intSlider.MinValue)) * size.X;
                    break;
                case ISlider<float> floatSlider:
                    pos.X += ((floatSlider.Value - floatSlider.MinValue) / (floatSlider.MaxValue - floatSlider.MinValue)) * size.X;
                    break;
                case ISlider<double> doubleSlider:
                    pos.X += (float)((doubleSlider.Value - doubleSlider.MinValue) / (doubleSlider.MaxValue - doubleSlider.MinValue)) * size.X;
                    break;
            }

            context.Renderer.DrawLine(pos, pos + new Vector2(0, size.Y), styleConfig.Slider.LineColor, styleConfig.Slider.LineWidth);
        }

        public override Vector2 GetSize(MenuBase context)
        {
            var totalSize = base.GetSize(context);
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;

            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<ISlider>();

            var font = styleConfig.Font;
            item.ValueSize = context.Renderer.MessureText(propValue.MaxValue.ToString(), font.Size, font.Family);
            totalSize.X += styleConfig.TextSpacing + item.ValueSize.X;

            return totalSize;
        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            return false;
        }

        public override void OnMouseMove(MenuBase context, MouseButtons buttons, Vector2 position)
        {
            var pos = context.Position;
            var size = context.RenderSize;

            var item = (MenuItemEntry)context;
            var propValue = item.ValueBinding.GetValue<ISlider>();

            var percentage = Math.Max(Math.Min((position.X - pos.X) / size.X, 1), 0);

            switch (propValue)
            {
                case Slider<int> intSlider:
                    intSlider.Value = (int)Math.Round(percentage * (intSlider.MaxValue - intSlider.MinValue)) + intSlider.MinValue;
                    break;
                case Slider<float> floatSlider:
                    floatSlider.Value = (int)Math.Round(percentage * (floatSlider.MaxValue - floatSlider.MinValue)) + floatSlider.MinValue;
                    break;
                case Slider<double> doubleSlider:
                    doubleSlider.Value = (int)Math.Round(percentage * (doubleSlider.MaxValue - doubleSlider.MinValue)) + doubleSlider.MinValue;
                    break;
            }
        }
    }
}