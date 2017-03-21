// <copyright file="EnsageWorkUnit.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service.Input;
    using Ensage.SDK.Service.Renderer.D2D;
    using Ensage.SDK.Service.Renderer.D3D;
    using Ensage.SDK.TargetSelector;

    [Export(typeof(IEnsage))]
    public class EnsageWorkUnit : IEnsage
    {
        [Import(typeof(ID2DRenderer))]
        public ID2DRenderer D2D { get; internal set; }

        [Import(typeof(ID3DRenderer))]
        public ID3DRenderer D3D { get; internal set; }

        [Import(typeof(IInput))]
        public IInput Input { get; internal set; }

        [Import(typeof(IOrbwalker))]
        public IOrbwalker Orbwalker { get; internal set; }

        [Import(typeof(ITargetSelector))]
        public ITargetSelector TargetSelector { get; internal set; }
    }
}