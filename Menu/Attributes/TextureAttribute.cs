// <copyright file="TextureAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    using Ensage.SDK.Renderer;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class TextureAttribute : Attribute
    {
        public TextureAttribute(string textureKey)
        {
            this.TextureKey = textureKey;
        }

        public string TextureKey { get; }

        public virtual void Load(IRenderManager renderer)
        {
        }
    }
}