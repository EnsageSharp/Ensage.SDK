// <copyright file="AutoAttackModeConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Config
{
    using System;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    public class AutoAttackModeConfig : IDisposable
    {
        private bool disposed;

        public AutoAttackModeConfig(MenuFactory parent, string name, uint key, bool hero, bool creep, bool neutral, bool building, bool deny, bool lasthit)
        {
            this.Factory = parent.Menu(name);

            this.Active = this.Factory.Item("Active", true);
            this.Key = this.Factory.Item("Key", new KeyBind(key));
            this.Deny = this.Factory.Item("Deny", deny);
            this.Deny.Item.Tooltip = "Deny creeps";
            this.Farm = this.Factory.Item("Lasthit", lasthit);
            this.Farm.Item.Tooltip = "Last hit creeps";
            this.Hero = this.Factory.Item("Hero", hero);
            this.Hero.Item.Tooltip = "Attack heroes";
            this.Building = this.Factory.Item("Building", building);
            this.Building.Item.Tooltip = "Attack buildings";
            this.Neutral = this.Factory.Item("Neutral", neutral);
            this.Neutral.Item.Tooltip = "Attack neutral creeps";
            this.Creep = this.Factory.Item("Creep", creep);
            this.Creep.Item.Tooltip = "Attack creeps";

            this.BonusMeleeRange = this.Factory.Item("Bonus Melee Range", new Slider(0, 0, 400));
            this.BonusRangedRange = this.Factory.Item("Bonus Ranged Range", new Slider(0, 0, 400));
        }

        public MenuItem<bool> Active { get; }

        public MenuItem<Slider> BonusMeleeRange { get; }

        public MenuItem<Slider> BonusRangedRange { get; }

        public MenuItem<bool> Building { get; }

        public MenuItem<bool> Creep { get; }

        public MenuItem<bool> Deny { get; }

        public MenuFactory Factory { get; }

        public MenuItem<bool> Farm { get; }

        public MenuItem<bool> Hero { get; }

        public MenuItem<KeyBind> Key { get; }

        public MenuItem<bool> Neutral { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Factory.Dispose();
            }

            this.disposed = true;
        }
    }
}