// <copyright file="IEnsage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using global::Ensage.SDK.Orbwalker;
    using global::Ensage.SDK.Service.Input;
    using global::Ensage.SDK.Service.Renderer.D2D;
    using global::Ensage.SDK.Service.Renderer.D3D;
    using global::Ensage.SDK.TargetSelector;

    public interface IEnsage
    {
        ID2DRenderer D2D { get; }

        ID3DRenderer D3D { get; }

        IInput Input { get; }

        IOrbwalker Orbwalker { get; }

        ITargetSelector TargetSelector { get; }
    }
}