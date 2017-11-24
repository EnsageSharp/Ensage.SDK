// <copyright file="SelectionView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;

    using SharpDX;

    [ExportView(typeof(Selection<>))]
    public class SelectionView : IView
    {
        public void Draw(MenuBase context)
        {
            // draw text
        }

        public Vector2 GetSize(MenuBase context)
        {
            throw new NotImplementedException();
        }

        public void OnClick(MenuBase context, Vector2 clickPosition)
        {
            throw new NotImplementedException();
        }
    }
}