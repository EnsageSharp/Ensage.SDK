// <copyright file="ViewRepository.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Menu.Views;

    [Export]
    public class ViewRepository
    {
        [Import]
        public Lazy<MenuView> MenuView { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IView, IViewMetadata>> Views { get; set; }

        public IView GetMenuView()
        {
            return this.MenuView.Value;
        }

        public IView GetView(Type type)
        {
            if (type.IsGenericType)
            {
                return this.Views.First(e => e.Metadata.Target == type.GetGenericTypeDefinition()).Value;
            }

            return this.Views.First(e => e.Metadata.Target == type).Value;
        }
    }
}