// <copyright file="IView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using SharpDX;

    public interface IView
    {
        void Draw(MenuBase context);

        Vector2 GetSize(MenuBase context);

        void OnClick(MenuBase context, Vector2 clickPosition);
    }
}