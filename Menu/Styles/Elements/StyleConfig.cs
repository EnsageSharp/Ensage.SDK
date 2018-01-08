// <copyright file="StyleConfig.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Menu.Styles.Elements;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class StyleConfig
    {
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
        ///     Gets or sets the style of <see cref="ImageToggler" /> UI element.
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

        /// <summary>
        ///     Gets or sets the style of the title bar.
        /// </summary>
        public TitleBarStyle TitleBar { get; set; } = new TitleBarStyle();

        /// <summary>
        ///     Gets or sets the style of the tooltips.
        /// </summary>
        public TooltipStyle Tooltip { get; set; } = new TooltipStyle();
    }
}