// <copyright file="MenuView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;
    using System.ComponentModel.Composition;

    using SharpDX;

    using Color = System.Drawing.Color;

    [Export]
    public class MenuView : IView
    {
        public void Draw(MenuBase context)
        {
            var pos = context.Position;
            var size = context.Size;

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
            context.Renderer.DrawTexture(activeStyle.Menu, new RectangleF(pos.X, pos.Y, size.X, size.Y), 0, 1.0f);
            if (context.IsHovered)
            {
                context.Renderer.DrawTexture(activeStyle.ArrowRight, new RectangleF(pos.X + size.X - 20, pos.Y + 12, 16, 16), 0, 1.0f);
            }
            else
            {
                context.Renderer.DrawTexture(activeStyle.ArrowRight, new RectangleF(pos.X + size.X - 24, pos.Y + 12, 16, 16), 0, 1.0f);
            }

            //context.Renderer.DrawRectangle(new RectangleF(pos.X, pos.Y, size.X, size.Y), color);
            context.Renderer.DrawText(pos, context.Name, Color.White, 16);
        }

        public Vector2 GetSize(MenuBase context)
        {
            return new Vector2(150, 30);
        }

        public void OnClick(MenuBase context, Vector2 clickPosition)
        {
            throw new NotImplementedException();
        }
    }
}