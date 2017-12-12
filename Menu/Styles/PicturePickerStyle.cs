namespace Ensage.SDK.Menu.Styles
{
    using SharpDX;

    using Color = System.Drawing.Color;

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
}