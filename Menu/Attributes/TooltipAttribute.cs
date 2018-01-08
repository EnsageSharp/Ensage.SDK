// <copyright file="TooltipAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class TooltipAttribute : Attribute
    {
        public TooltipAttribute(string tooltip = null)
        {
            this.Text = tooltip;
        }

        public string Text { get; set; }
    }
}