// <copyright file="StringView.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Entries;

    using SharpDX;

    [ExportView(typeof(string))]
    public class StringView : View
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
            context.Renderer.DrawText(pos, context.Name, font.Color, font.Size, font.Family);

            var textSize = context.Renderer.MessureText(item.ValueBinding.GetValue<string>(), font.Size, font.Family);
            pos.X = (context.Position.X + size.X) - border.Thickness[2] - styleConfig.TextSpacing - textSize.X;
            context.Renderer.DrawText(pos, item.ValueBinding.GetValue<string>(), font.Color, font.Size, font.Family);
        }

        public override Vector2 GetSize(MenuBase context)
        {
            var totalSize = base.GetSize(context);
            var styleConfig = context.MenuConfig.GeneralConfig.ActiveStyle.Value.StyleConfig;
            var font = styleConfig.Font;

            var item = (MenuItemEntry)context;
            item.ValueSize = context.Renderer.MessureText(item.ValueBinding.GetValue<string>(), font.Size, font.Family);
            totalSize.X += styleConfig.TextSpacing + item.ValueSize.X;

            return totalSize;
        }

        public override bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition)
        {
            return false;
        }
    }
}