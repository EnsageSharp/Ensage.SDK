// <copyright file="TextureResourceAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    using Ensage.SDK.Renderer;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TextureResourceAttribute : TextureAttribute
    {
        public TextureResourceAttribute(string textureKey, string resourceFileName)
            : base(textureKey)
        {
            this.ResourceFileName = resourceFileName;
        }

        public string ResourceFileName { get; set; }

        public override void Load(IRendererManager renderer)
        {
            renderer.TextureManager.LoadFromResource(this.TextureKey, this.ResourceFileName);
        }
    }
}