// <copyright file="BorderStyle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
    using SharpDX;

    using Color = System.Drawing.Color;

    public class BorderStyle
    {
        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        public Color Color { get; set; } = Color.Transparent;

        /// <summary>
        /// Gets or sets the thickness of the border: left, top, right, bottom.
        /// </summary>
        public Vector4 Thickness { get; set; } = new Vector4(5, 7, 1, 7);
    }
}