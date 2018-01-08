// <copyright file="SliderStyle.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles.Elements
{
    using System.Drawing;

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
}