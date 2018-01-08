// <copyright file="TextureNotFoundException.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;

    public class TextureNotFoundException : Exception
    {
        public TextureNotFoundException(string textureKey)
        {
            this.TextureKey = textureKey;
        }

        public string TextureKey { get; }
    }
}