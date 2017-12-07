// <copyright file="StyleConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
    using SharpDX;

    using Color = System.Drawing.Color;

    public class StyleConfig
    {
        /// <summary>
        ///     Gets or sets the arrow size of the menu entries.
        /// </summary>
        public Vector2 ArrowSize { get; set; }

        /// <summary>
        ///     Gets or sets the size of the space between the menu text and the content following.
        /// </summary>
        public float TextSpacing { get; set; }

        /// <summary>
        ///     Gets or sets information about the border.
        /// </summary>
        public BorderStyle Border { get; set; } = new BorderStyle();

        /// <summary>
        ///     Gets or sets information about the font style of the menu.
        /// </summary>
        public FontStyle Font { get; set; } = new FontStyle();

        /// <summary>
        ///     Gets or sets the color for unselected menu items.
        /// </summary>
        public Color LineColor { get; set; }

        /// <summary>
        ///     Gets or sets the color for selected menu items.
        /// </summary>
        public Color SelectedLineColor { get; set; }

        /// <summary>
        ///     Gets or sets the line width of selected menu items.
        /// </summary>
        public float LineWidth { get; set; }
    }
}