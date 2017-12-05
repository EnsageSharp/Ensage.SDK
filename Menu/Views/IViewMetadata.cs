// <copyright file="IViewMetadata.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;

    public interface IViewMetadata
    {
        Type Target { get; }
    }
}