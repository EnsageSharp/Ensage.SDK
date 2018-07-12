// <copyright file="TextureEnsageAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    using Ensage.SDK.Renderer;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TextureEnsageAttribute : TextureAttribute
    {
        private static readonly string GamePath = Game.GamePath;

        public TextureEnsageAttribute(string fileName)
            : base(fileName)
        {
            this.FileName = fileName;
        }

        public TextureEnsageAttribute(string textureKey, string fileName)
            : base(textureKey)
        {
            this.FileName = fileName;
        }

        public string FileName { get; }

        public override void Load(IRendererManager renderer)
        {
            renderer.TextureManager.LoadFromFile(this.TextureKey, $@"{GamePath}\game\dota\materials\ensage_ui\{this.FileName}");
        }
    }
}