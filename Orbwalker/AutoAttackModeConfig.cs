// <copyright file="AutoAttackModeConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class AutoAttackModeConfig
    {
        public AutoAttackModeConfig(MenuFactory parent, string name, uint key, bool hero, bool creep, bool neutral, bool building, bool deny, bool lasthit)
        {
            this.Factory = parent.Menu(name);

            this.Key = this.Factory.Item("Key", new KeyBind(key, KeyBindType.Press));
            this.Deny = this.Factory.Item("Deny", deny);
            this.Farm = this.Factory.Item("Lasthit", lasthit);
            this.Hero = this.Factory.Item("Hero", hero);
            this.Building = this.Factory.Item("Building", building);
            this.Neutral = this.Factory.Item("Neutral", neutral);
            this.Creep = this.Factory.Item("Creep", creep);
        }

        public MenuItem<bool> Building { get; }

        public MenuItem<bool> Creep { get; }

        public MenuItem<bool> Deny { get; }

        public MenuFactory Factory { get; }

        public MenuItem<bool> Farm { get; }

        public MenuItem<bool> Hero { get; }

        public MenuItem<KeyBind> Key { get; }

        public MenuItem<bool> Neutral { get; }
    }
}