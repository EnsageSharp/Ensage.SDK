// <copyright file="IView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using Ensage.SDK.Input;

    using SharpDX;

    public interface IView
    {
        void Draw(MenuBase context);

        Vector2 GetSize(MenuBase context);

        bool OnClick(MenuBase context, MouseButtons buttons, Vector2 clickPosition);
    }
}