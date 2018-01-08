// <copyright file="FontStyle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles.Elements
{
    using System.Drawing;

    public class FontStyle
    {
        /// <summary>
        ///     Gets or sets the color of the text.
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        ///     Gets or sets the font family.
        /// </summary>
        public string Family { get; set; } = "Calibri";

        /// <summary>
        ///     Gets or sets the size of the font.
        /// </summary>
        public int Size { get; set; } = 16;
    }
}