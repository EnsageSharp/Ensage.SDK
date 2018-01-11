// <copyright file="PermaConfig.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
    using Ensage.SDK.Menu.Styles.Elements;

    using SharpDX;

    public class PermaConfig
    {
        public PermaConfig()
        {
            this.Font.Size = 13;
            this.Border.Thickness = new Vector4(2, 3, 2, 3);
        }

        public Vector2 ArrowSize { get; set; } = new Vector2(16, 16);

        public BorderStyle Border { get; set; } = new BorderStyle();

        public FontStyle Font { get; set; } = new FontStyle();

        public float LineWidth { get; set; } = 5;
    }
}