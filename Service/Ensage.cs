// <copyright file="Ensage.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System.ComponentModel.Composition;

    using global::Ensage.SDK.Orbwalker;
    using global::Ensage.SDK.Service.Input;
    using global::Ensage.SDK.Service.Renderer;
    using global::Ensage.SDK.TargetSelector;

    public class Ensage : IEnsage
    {
        [Import(typeof(IDirect2DRenderer))]
        public IDirect2DRenderer D2D { get; }

        [Import(typeof(IDirect3DRenderer))]
        public IDirect3DRenderer D3D { get; }

        [Import(typeof(IInput))]
        public IInput Input { get; internal set; }

        [Import(typeof(IOrbwalker))]
        public IOrbwalker Orbwalker { get; internal set; }

        [Import(typeof(ITargetSelector))]
        public ITargetSelector TargetSelector { get; internal set; }
    }
}