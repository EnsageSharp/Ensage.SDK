// <copyright file="SelectionView.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Views
{
    using System;

    using Ensage.SDK.Menu.Attributes;
    using Ensage.SDK.Menu.Items;

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
            return new Vector2(150, 20);
        }

        public void OnClick(MenuBase context, Vector2 clickPosition)
        {
            throw new NotImplementedException();
        }
    }
}