// <copyright file="IEnsage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service.Input;
    using Ensage.SDK.Service.Renderer.D2D;
    using Ensage.SDK.Service.Renderer.D3D;
    using Ensage.SDK.TargetSelector;

    public interface IEnsage
    {
        ID2DRenderer D2D { get; }

        ID3DRenderer D3D { get; }

        IInput Input { get; }

        IOrbwalker Orbwalker { get; }

        ITargetSelector TargetSelector { get; }
    }
}