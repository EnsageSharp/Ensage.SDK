// <copyright file="ITextureManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;
    using System.IO;
    using System.Reflection;

    using SharpDX;

    public interface ITextureManager : IDisposable
    {
        bool LoadFromFile(string textureKey, string file);

        bool LoadFromMemory(string textureKey, byte[] data);

        bool LoadFromStream(string textureKey, Stream stream);

        bool LoadFromDota(string textureKey, string file);

        bool LoadFromResource(string textureKey, string file, Assembly assembly = null);

        Vector2 GetTextureSize(string textureKey);
    }
}