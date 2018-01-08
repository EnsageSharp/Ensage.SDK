// <copyright file="TooltipStyle.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles.Elements
{
    using SharpDX;

    using Color = System.Drawing.Color;

    public class TooltipStyle
    {
        public TooltipStyle()
        {
            this.Font.Size = 11;
            this.Border.Color = Color.Black;
            this.Border.Thickness = new Vector4(2, 2, 2, 2);
        }

        public BorderStyle Border { get; set; } = new BorderStyle();

        public FontStyle Font { get; set; } = new FontStyle();
    }
}