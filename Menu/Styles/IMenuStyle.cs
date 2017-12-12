// <copyright file="IMenuStyle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Styles
{
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

        string ToString();
    }
}