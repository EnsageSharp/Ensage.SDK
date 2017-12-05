// <copyright file="StyleRepository.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Menu.Styles;

    [Export]
    public class StyleRepository
    {
        private IMenuStyle activeStyle;

        public IMenuStyle ActiveStyle
        {
            get
            {
                if (activeStyle == null)
                {
                    return DefaultMenuStyle;
                }

                return activeStyle;
            }

            set
            {
                activeStyle = value;
            }
        }

        [Import]
        public DefaultMenuStyle DefaultMenuStyle { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<IMenuStyle> Styles { get; set; }

        public IMenuStyle GetStyle(string name)
        {
            return Styles.First(e => e.Name == name);
        }
    }
}