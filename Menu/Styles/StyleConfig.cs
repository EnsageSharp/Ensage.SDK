// <copyright file="StyleConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
    using Ensage.SDK.Menu.Items;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class SliderStyle
    {
        /// <summary>
        ///     Color of the currently selected slider line.
        /// </summary>
        public Color LineColor { get; set; } = Color.DarkOrange;

        /// <summary>
        ///     Width of the currently selected slider line value.
        /// </summary>
        public float LineWidth { get; set; } = 2;
    }

    public class PicturePickerStyle
    {
        /// <summary>
        ///     Color of unselected items.
        /// </summary>
        public Color Color { get; set; } = Color.DarkRed;

        /// <summary>
        ///     Line width of the colored rectangle around each picture.
        /// </summary>
        public float LineWidth { get; set; } = 3;

        /// <summary>
        ///     Size of each picture.
        /// </summary>
        public Vector2 PictureSize { get; set; } = new Vector2(20, 20);

        /// <summary>
        ///     Color of each selected item.
        /// </summary>
        public Color SelectedColor { get; set; } = Color.GreenYellow;
    }

    public class TitleBarStyle
    {
        public BorderStyle Border = new BorderStyle();

        public FontStyle Font = new FontStyle();
    }

    public class StyleConfig
    {
        /// <summary>
        /// Gets or sets the style of the title bar.
        /// </summary>
        public TitleBarStyle TitleBar = new TitleBarStyle();

        /// <summary>
        ///     Gets or sets the arrow size of the menu entries.
        /// </summary>
        public Vector2 ArrowSize { get; set; } = new Vector2(20, 20);

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
        public Color LineColor { get; set; } = Color.Black;

        /// <summary>
        ///     Gets or sets the line width of selected menu items.
        /// </summary>
        public float LineWidth { get; set; } = 3;

        /// <summary>
        ///     Gets or sets the style of <see cref="Items.PicturePicker" /> UI element.
        /// </summary>
        public PicturePickerStyle PicturePicker { get; set; } = new PicturePickerStyle();

        /// <summary>
        ///     Gets or sets the color for selected menu items.
        /// </summary>
        public Color SelectedLineColor { get; set; } = Color.DarkOrange;

        /// <summary>
        ///     Gets or sets the style of the <see cref="Items.Slider" /> UI element.
        /// </summary>
        public SliderStyle Slider { get; set; } = new SliderStyle();

        /// <summary>
        ///     Gets or sets the size of the space between the menu text and the content following.
        /// </summary>
        public float TextSpacing { get; set; } = 5;
    }
}