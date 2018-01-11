// <copyright file="TextureFileAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    using Ensage.SDK.Renderer;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TextureFileAttribute : TextureAttribute
    {
        public TextureFileAttribute(string textureKey, string fileName)
            : base(textureKey)
        {
            this.FileName = fileName;
        }

        public string FileName { get; }

        public override void Load(IRendererManager renderer)
        {
            renderer.TextureManager.LoadFromFile(this.TextureKey, this.FileName);
        }
    }
}