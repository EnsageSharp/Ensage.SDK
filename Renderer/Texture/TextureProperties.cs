// <copyright file="TextureProperties.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Texture
{
    using SharpDX;

    public struct TextureProperties
    {
        public static TextureProperties Round
        {
            get
            {
                return new TextureProperties
                {
                    Rounded = true
                };
            }
        }

        public int Brightness { get; set; }

        public bool Rounded { get; set; }

        public bool Squared { get; set; }

        public bool Sliced { get; set; }

        public Vector4 ColorRatio { get; set; }
    }
}