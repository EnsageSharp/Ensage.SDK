// <copyright file="IViewMetadata.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    public interface IViewMetadata
    {
        Type Target { get; }
    }
}