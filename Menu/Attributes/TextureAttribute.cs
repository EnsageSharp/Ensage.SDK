// <copyright file="TooltipAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TexureAttribute : Attribute
    {
        public TexureAttribute(string textureKey)
        {
            this.TextureKey = textureKey;
        }

        public string TextureKey { get; set; }
    }
}