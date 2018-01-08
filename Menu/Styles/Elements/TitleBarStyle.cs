// <copyright file="TitleBarStyle.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
    using Ensage.SDK.Menu.Styles.Elements;

    using SharpDX;

    public class TitleBarStyle
    {
        public TitleBarStyle()
        {
            this.Font.Size = 13;
            this.Border.Thickness = new Vector4(30, 2, 30, 2);
        }

        public BorderStyle Border { get; set; } = new BorderStyle();

        public FontStyle Font { get; set; } = new FontStyle();
    }
}