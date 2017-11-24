// <copyright file="BooleanView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    using SharpDX;

    using Color = System.Drawing.Color;

    [ExportView(typeof(bool))]
    public class BooleanView : IView
    {
        public void Draw(MenuBase context)
        {
            var pos = context.Position;
            var size = context.Size;

            System.Drawing.Color color;
            if (context.IsHovered)
            {
                color = System.Drawing.Color.Yellow;
            }
            else
            {
                color = System.Drawing.Color.White;
            }

            context.Renderer.DrawRectangle(new RectangleF(pos.X, pos.Y, size.X, size.Y), color);

            var text = context.Name + ((bool)((MenuItemEntry)context).Value ? "On" : "Off");
            context.Renderer.DrawText(pos, text, Color.YellowGreen);
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