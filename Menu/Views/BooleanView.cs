// <copyright file="BooleanView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;

    using SharpDX;

    using Color = System.Drawing.Color;

    [ExportView(typeof(bool))]
    public class BooleanView : IView
    {
        public void Draw(MenuBase context)
        {
            var pos = context.Position;
            var size = context.Size;

            var item = context as MenuItemEntry;

            Color color;
            if (context.IsHovered)
            {
                color = Color.Yellow;
            }
            else
            {
                color = Color.White;
            }

            var activeStyle = context.StyleRepository.ActiveStyle;
            context.Renderer.DrawTexture(activeStyle.Item, new RectangleF(pos.X, pos.Y, size.X, size.Y));

            context.Renderer.DrawText(pos, context.Name, Color.YellowGreen);
            context.Renderer.DrawTexture(activeStyle.Slider, new RectangleF(pos.X + size.X - 30, pos.Y, 20, 5));
            if (item.PropertyBinding.GetValue<bool>())
            {
                context.Renderer.DrawTexture(activeStyle.Checked, new RectangleF(pos.X + size.X - 20, pos.Y, 20, 5));
            }
            else
            {
                context.Renderer.DrawTexture(activeStyle.Unchecked, new RectangleF(pos.X + size.X - 10, pos.Y, 20, 5));
            }
        }

        public Vector2 GetSize(MenuBase context)
        {
            return new Vector2(150, 20);
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