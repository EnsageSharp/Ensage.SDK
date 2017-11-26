// <copyright file="BitmapNotFoundException.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;

    public class BitmapNotFoundException : Exception
    {
        public BitmapNotFoundException(string bitmapKey)
        {
            this.BitmapKey = bitmapKey;
        }

        public string BitmapKey { get; }
    }
}