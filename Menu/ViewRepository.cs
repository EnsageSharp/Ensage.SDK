// <copyright file="ViewRepository.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Menu.ValueBinding;
    using Ensage.SDK.Menu.Views;

    [Export]
    public class ViewRepository
    {
        [Import]
        public Lazy<MenuView> MenuView { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<View, IViewMetadata>> Views { get; set; }

        public View GetMenuView()
        {
            return this.MenuView.Value;
        }

        public View GetView(Type type)
        {
            if (type.IsGenericType)
            {
                // get specialized view of generic type
                var view = this.Views.FirstOrDefault(e => e.Metadata.Target == type)?.Value;
                if (view != null)
                {
                    return view;
                }

                // get general view of generic type
                view = this.Views.FirstOrDefault(e => e.Metadata.Target == type.GetGenericTypeDefinition())?.Value;
                if (view != null)
                {
                    return view;
                }

                // get view for generic argument type
                return this.Views.First(e => e.Metadata.Target == type.GenericTypeArguments.First()).Value;
            }

            return this.Views.First(e => e.Metadata.Target == type).Value;
        }
    }
}