// <copyright file="ITextureManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;
    using System.IO;

    public interface ITextureManager : IDisposable
    {
        bool LoadFromFile(string bitmapKey, string file);

        bool LoadFromMemory(string bitmapKey, byte[] data);

        bool LoadFromStream(string bitmapKey, Stream stream);
    }
}