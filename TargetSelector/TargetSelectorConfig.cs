// <copyright file="TargetSelectorConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using Ensage.SDK.Menu;

    public class TargetSelectorConfig
    {
        public TargetSelectorConfig(MenuFactory parent, string name, bool hero, bool creep, bool neutral, bool building, bool deny, bool farm)
        {
            this.Factory = parent.Menu(name);

            this.Hero = this.Factory.Item("Hero", hero);
            this.Creep = this.Factory.Item("Creep", creep);
            this.Neutral = this.Factory.Item("Neutral", neutral);
            this.Building = this.Factory.Item("Building", building);
            this.Deny = this.Factory.Item("Deny", deny);
            this.Farm = this.Factory.Item("Farm", farm);
        }

        public MenuItem<bool> Building { get; }

        public MenuItem<bool> Creep { get; }

        public MenuItem<bool> Deny { get; }

        public MenuFactory Factory { get; }

        public MenuItem<bool> Farm { get; }

        public MenuItem<bool> Hero { get; }

        public MenuItem<bool> Neutral { get; }
    }
}