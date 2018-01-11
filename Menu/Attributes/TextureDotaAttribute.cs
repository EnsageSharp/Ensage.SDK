// <copyright file="TextureDotaAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    using Ensage.SDK.Renderer;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TextureDotaAttribute : TextureAttribute
    {
        public TextureDotaAttribute(string textureKey, string dotaFileName)
            : base(textureKey)
        {
            this.DotaFileName = dotaFileName;
        }

        public string DotaFileName { get; }

        public override void Load(IRendererManager renderer)
        {
            renderer.TextureManager.LoadFromDota(this.TextureKey, this.DotaFileName);
        }
    }
}