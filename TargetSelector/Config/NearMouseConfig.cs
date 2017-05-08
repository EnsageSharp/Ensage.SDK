// <copyright file="NearMouseConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Config
{
    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class NearMouseConfig
    {
        public NearMouseConfig(MenuFactory parent)
        {
            this.Factory = parent.Menu("Near Mouse");
            this.Range = this.Factory.Item("Range", new Slider(800, 100, 2000));
        }

        public MenuFactory Factory { get; }

        public MenuItem<Slider> Range { get; }
    }
}