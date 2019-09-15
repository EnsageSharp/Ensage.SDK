// <copyright file="IMenuStyle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles.Elements
{
    using System.Runtime.CompilerServices;

    using Ensage.SDK.Renderer;

    public interface IMenuStyle
    {
        string ArrowLeft { get; }

        string ArrowLeftHover { get; }

        string ArrowRight { get; }

        string ArrowRightHover { get; }

        string Checked { get; }

        string Item { get; }

        string Menu { get; }

        string Name { get; }

        string Slider { get; }

        StyleConfig StyleConfig { get; }

        string TitleBar { get; }

        string Unchecked { get; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        void LoadResources(IRenderManager renderer);

        string ToString();
    }
}