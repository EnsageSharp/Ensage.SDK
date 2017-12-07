// <copyright file="FontStyle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System.Drawing;

    public class FontStyle
    {
        public FontStyle()
        {
        }

        public FontStyle(string family, int size, Color color)
        {
            Family = family;
            Size = size;
            Color = color;
        }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        public Color Color { get; set; }
    }
}