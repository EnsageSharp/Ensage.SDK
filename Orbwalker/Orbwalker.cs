// <copyright file="Orbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public class Orbwalker
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Orbwalker _Instance;

        public OrbwalkingMode Mode = OrbwalkingMode.None;

        private readonly HashSet<NetworkActivity> attackActivityList = new HashSet<NetworkActivity>
                                                                       {
                                                                           NetworkActivity.Attack,
                                                                           NetworkActivity.Attack2,
                                                                           NetworkActivity.AttackEvent
                                                                       };

        private Menu clearMenu;

        private Menu comboMenu;

        private Menu denyMenu;

        private Menu farmMenu;

        private Menu hotkeys;

        private Menu menu;

        private Menu mixedMenu;

        public Orbwalker(Unit owner)
        {
            this.Owner = owner;
        }

        public enum OrbwalkingMode
        {
            Combo,

            LaneClear,

            LastHit,

            Deny,

            Mixed,

            None
        }

        public MenuItem Clear { get; set; }

        public MenuItem Combo { get; set; }

        public MenuItem Deny { get; set; }

        public MenuItem Farm { get; set; }

        public bool LaneClearRateLimitResult { get; set; }

        public float LaneClearRateLimitTime { get; set; }

        public float LastAttackOrderIssuedTime { get; set; }

        public float LastAttackTime { get; set; }

        public float LastMoveOrderIssuedTime { get; set; }

        public MenuItem Mixed { get; set; }

        public Unit Owner { get; set; }

        public float TurnEndTime { get; set; }

        private bool _Initialized { get; set; }

        private float LastAutoAttackRangeRadius { get; set; }

        public static Orbwalker Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Orbwalker(ObjectManager.LocalHero);
            }

            return _Instance;
        }

        public bool Attack(Unit unit)
        {
            var time = Game.RawGameTime;
            if ((time - this.LastAttackOrderIssuedTime) < 0.005f)
            {
                return false;
            }

            this.TurnEndTime = Game.RawGameTime + (Game.Ping / 2000f) + (float)this.Owner.TurnTime(unit.NetworkPosition) + 0.1f;
            this.Owner.Attack(unit);
            return true;
        }

        public bool CanAttack(Unit target)
        {
            var rotationTime = this.Owner.TurnTime(target.NetworkPosition);
            return this.Owner.CanAttack() && ((Game.RawGameTime + 0.1f + rotationTime + (Game.Ping / 2000f)) - this.LastAttackTime) > (1f / this.Owner.AttacksPerSecond);
        }

        public bool CanMove()
        {
            return (((Game.RawGameTime - 0.1f) + (Game.Ping / 2000f)) - this.LastAttackTime) > this.Owner.AttackPoint();
        }

        public bool Load()
        {
            if (this._Initialized)
            {
                return false;
            }

            this._Initialized = true;

            this.menu = new Menu("Orbwalker BETA", "Orbwalker BETA", true);

            this.hotkeys = this.menu.AddSubMenu(new Menu("Hotkeys", "Hotkeys"));
            this.Combo = this.hotkeys.AddItem(new MenuItem("Hotkeys.Combo", "Combo").SetValue(new KeyBind(32, KeyBindType.Press)));
            this.Mixed = this.hotkeys.AddItem(new MenuItem("Hotkeys.Mixed", "Mixed").SetValue(new KeyBind('G', KeyBindType.Press)));
            this.Clear = this.hotkeys.AddItem(new MenuItem("Hotkeys.Clear", "Clear").SetValue(new KeyBind('T', KeyBindType.Press)));
            this.Farm = this.hotkeys.AddItem(new MenuItem("Hotkeys.Farm", "Farm").SetValue(new KeyBind('F', KeyBindType.Press)));
            this.Deny = this.hotkeys.AddItem(new MenuItem("Hotkeys.Deny", "Deny").SetValue(new KeyBind('D', KeyBindType.Press)));

            this.comboMenu = this.menu.AddSubMenu(new Menu("Combo", "Combo"));
            this.comboMenu.AddItem(new MenuItem("Combo.Hero", "Hero").SetValue(true));
            this.comboMenu.AddItem(new MenuItem("Combo.Tower", "Tower").SetValue(false));
            this.comboMenu.AddItem(new MenuItem("Combo.Building", "Building").SetValue(false));
            this.comboMenu.AddItem(new MenuItem("Combo.Farm", "Farm").SetValue(false));
            this.comboMenu.AddItem(new MenuItem("Combo.Deny", "Deny").SetValue(false));
            this.comboMenu.AddItem(new MenuItem("Combo.Jungle", "Jungle").SetValue(false));
            this.comboMenu.AddItem(new MenuItem("Combo.Creep", "Creep").SetValue(false));

            this.mixedMenu = this.menu.AddSubMenu(new Menu("Mixed", "Mixed"));
            this.mixedMenu.AddItem(new MenuItem("Mixed.Hero", "Hero").SetValue(true));
            this.mixedMenu.AddItem(new MenuItem("Mixed.Tower", "Tower").SetValue(false));
            this.mixedMenu.AddItem(new MenuItem("Mixed.Building", "Building").SetValue(false));
            this.mixedMenu.AddItem(new MenuItem("Mixed.Farm", "Farm").SetValue(true));
            this.mixedMenu.AddItem(new MenuItem("Mixed.Deny", "Deny").SetValue(false));
            this.mixedMenu.AddItem(new MenuItem("Mixed.Jungle", "Jungle").SetValue(false));
            this.mixedMenu.AddItem(new MenuItem("Mixed.Creep", "Creep").SetValue(false));

            this.clearMenu = this.menu.AddSubMenu(new Menu("Clear", "Clear"));
            this.clearMenu.AddItem(new MenuItem("Clear.Hero", "Hero").SetValue(true));
            this.clearMenu.AddItem(new MenuItem("Clear.Tower", "Tower").SetValue(true));
            this.clearMenu.AddItem(new MenuItem("Clear.Building", "Building").SetValue(true));
            this.clearMenu.AddItem(new MenuItem("Clear.Farm", "Farm").SetValue(false));
            this.clearMenu.AddItem(new MenuItem("Clear.Deny", "Deny").SetValue(false));
            this.clearMenu.AddItem(new MenuItem("Clear.Jungle", "Jungle").SetValue(true));
            this.clearMenu.AddItem(new MenuItem("Clear.Creep", "Creep").SetValue(true));

            this.farmMenu = this.menu.AddSubMenu(new Menu("Farm", "Farm"));
            this.farmMenu.AddItem(new MenuItem("Farm.Hero", "Hero").SetValue(false));
            this.farmMenu.AddItem(new MenuItem("Farm.Tower", "Tower").SetValue(false));
            this.farmMenu.AddItem(new MenuItem("Farm.Building", "Building").SetValue(false));
            this.farmMenu.AddItem(new MenuItem("Farm.Farm", "Farm").SetValue(true));
            this.farmMenu.AddItem(new MenuItem("Farm.Deny", "Deny").SetValue(false));
            this.farmMenu.AddItem(new MenuItem("Farm.Jungle", "Jungle").SetValue(false));
            this.farmMenu.AddItem(new MenuItem("Farm.Creep", "Creep").SetValue(false));

            this.denyMenu = this.menu.AddSubMenu(new Menu("Deny", "Deny"));
            this.denyMenu.AddItem(new MenuItem("Deny.Hero", "Hero").SetValue(false));
            this.denyMenu.AddItem(new MenuItem("Deny.Tower", "Tower").SetValue(false));
            this.denyMenu.AddItem(new MenuItem("Deny.Building", "Building").SetValue(false));
            this.denyMenu.AddItem(new MenuItem("Deny.Farm", "Farm").SetValue(false));
            this.denyMenu.AddItem(new MenuItem("Deny.Deny", "Deny").SetValue(true));
            this.denyMenu.AddItem(new MenuItem("Deny.Jungle", "Jungle").SetValue(false));
            this.denyMenu.AddItem(new MenuItem("Deny.Creep", "Creep").SetValue(false));

            this.menu.AddToMainMenu();

            HealthPredictionOLD.Instance().Load();
            Game.OnIngameUpdate += this.GameDispatcherOnOnIngameUpdate;
            Entity.OnInt32PropertyChange += this.Hero_OnInt32PropertyChange;
            return true;
        }

        public bool Move(Vector3 position)
        {
            var time = Game.RawGameTime;
            if ((time - this.LastMoveOrderIssuedTime) < 0.005f)
            {
                return false;
            }

            this.LastMoveOrderIssuedTime = Game.RawGameTime;

            this.Owner.Move(position);
            return true;
        }

        public bool Unload()
        {
            if (!this._Initialized)
            {
                return false;
            }

            this._Initialized = false;

            HealthPredictionOLD.Instance().Unload();
            Game.OnIngameUpdate -= this.GameDispatcherOnOnIngameUpdate;
            Entity.OnInt32PropertyChange -= this.Hero_OnInt32PropertyChange;

            return true;
        }

        private void GameDispatcherOnOnIngameUpdate(EventArgs args)
        {
            try
            {
                this.Mode = OrbwalkingMode.None;

                this.Owner.DrawRange("attackRange", this.Owner.AttackRange(this.Owner));

                // no spamerino
                if (Game.IsPaused || Game.IsChatOpen)
                {
                    return;
                }

                Unit target = null;

                if (this.Combo.IsActive())
                {
                    this.Mode = OrbwalkingMode.Combo;
                    target = this.GetTargets(this.comboMenu).FirstOrDefault();
                }
                else if (this.Clear.IsActive())
                {
                    this.Mode = OrbwalkingMode.LaneClear;
                    target = this.GetTargets(this.clearMenu).FirstOrDefault();
                }
                else if (this.Mixed.IsActive())
                {
                    this.Mode = OrbwalkingMode.Mixed;
                    target = this.GetTargets(this.mixedMenu).FirstOrDefault();
                }
                else if (this.Farm.IsActive())
                {
                    this.Mode = OrbwalkingMode.LastHit;
                    target = this.GetTargets(this.farmMenu).FirstOrDefault();
                }
                else if (this.Deny.IsActive())
                {
                    this.Mode = OrbwalkingMode.Deny;
                    target = this.GetTargets(this.denyMenu).FirstOrDefault();
                }

                if (this.Mode == OrbwalkingMode.None)
                {
                    return;
                }

                // turning
                if (this.TurnEndTime > Game.RawGameTime)
                {
                    return;
                }

                if ((target == null || !this.CanAttack(target)) && this.CanMove())
                {
                    this.Move(Game.MousePosition);
                    return;
                }

                if (target != null && this.CanAttack(target))
                {
                    this.Attack(target);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private ParallelQuery<Unit> GetBuilding()
        {
            // towers // barracks shrines
            var barracks = ObjectManager.GetEntitiesParallel<Building>().Where(unit => this.Owner.IsValidOrbwalkingTarget(unit)).OfType<Unit>();

            return barracks;
        }

        private ParallelQuery<Unit> GetCreep()
        {
            // creeps
            var creep = CreepManager.Instance().GetCreeps().Where(unit => unit.Team != this.Owner.Team && this.Owner.IsValidOrbwalkingTarget(unit));

            return creep;
        }

        private ParallelQuery<Unit> GetHero()
        {
            var targetHero =
                ObjectManager.GetEntitiesParallel<Hero>()
                             .Where(unit => this.Owner.IsValidOrbwalkingTarget(unit) && unit.Team != this.Owner.Team && unit.Distance2D(Game.MousePosition) < 2000)
                             .OrderByDescending(unit => unit.Distance2D(Game.MousePosition))
                             .OfType<Unit>();

            return targetHero;
        }

        private ParallelQuery<Unit> GetJungle()
        {
            // jungle mobs
            var jungleMob = CreepManager.Instance().GetCreeps().Where(unit => this.Owner.IsValidOrbwalkingTarget(unit) && unit.IsNeutral);

            return jungleMob;
        }

        private ParallelQuery<Unit> GetKillableCreep()
        {
            // killable creeps
            var killableCreep =
                CreepManager.Instance()
                            .GetCreeps()
                            .Where(
                                unit =>
                                    this.Owner.IsValidOrbwalkingTarget(unit)
                                    && HealthPredictionOLD.Instance().GetPredictedHealth(unit, this.Owner.GetAutoAttackArrivalTime(unit) + (Game.Ping / 2000f))
                                    < this.Owner.GetAttackDamage(unit, true));

            return killableCreep;
        }

        private ParallelQuery<Unit> GetKillableDenyCreep()
        {
            // check if we should wait because there is a creep that will be killable soon
            // if (Game.RawGameTime - this.LaneClearRateLimitTime > 0.25f)
            // {
            // this.LaneClearRateLimitResult = this.ShouldWait();
            // this.LaneClearRateLimitTime = Game.RawGameTime;
            // }

            // if (this.LaneClearRateLimitResult)
            // {
            // return null;
            // }

            // denyCreep creeps
            var denyCreep =
                CreepManager.Instance()
                            .GetCreeps()
                            .Where(
                                unit =>
                                    unit.IsValid && this.Owner.IsValidOrbwalkingTarget(unit) && unit.HealthPercent() < 0.5f
                                    && HealthPredictionOLD.Instance().GetPredictedHealth(unit, this.Owner.GetAutoAttackArrivalTime(unit) + (Game.Ping / 2000f))
                                    < this.Owner.GetAttackDamage(unit, true));

            return denyCreep;
        }

        private IEnumerable<Unit> GetTargets(Menu menu)
        {
            var prefix = menu.Name;

            if (menu.Items.First(i => i.Name == $"{prefix}.Hero").IsActive())
            {
                foreach (var unit in this.GetHero())
                {
                    yield return unit;
                }
            }

            if (menu.Items.First(i => i.Name == $"{prefix}.Tower").IsActive())
            {
                foreach (var unit in this.GetTower())
                {
                    yield return unit;
                }
            }

            if (menu.Items.First(i => i.Name == $"{prefix}.Building").IsActive())
            {
                foreach (var unit in this.GetBuilding())
                {
                    yield return unit;
                }
            }

            if (menu.Items.First(i => i.Name == $"{prefix}.Farm").IsActive())
            {
                foreach (var unit in this.GetKillableCreep())
                {
                    yield return unit;
                }
            }

            if (menu.Items.First(i => i.Name == $"{prefix}.Deny").IsActive())
            {
                foreach (var unit in this.GetKillableDenyCreep())
                {
                    yield return unit;
                }
            }

            if (menu.Items.First(i => i.Name == $"{prefix}.Jungle").IsActive())
            {
                foreach (var unit in this.GetJungle())
                {
                    yield return unit;
                }
            }

            if (menu.Items.First(i => i.Name == $"{prefix}.Creep").IsActive())
            {
                foreach (var unit in this.GetCreep())
                {
                    yield return unit;
                }
            }
        }

        private IEnumerable<T> GetTargetSequence<T>(params Func<ParallelQuery<T>>[] selectors) where T : Unit
        {
            foreach (var selector in selectors)
            {
                var iter = selector();

                if (iter == null)
                {
                    continue;
                }

                foreach (var unit in iter)
                {
                    if (unit == null)
                    {
                        continue;
                    }

                    yield return unit;
                }
            }
        }

        private ParallelQuery<Unit> GetTower()
        {
            var tower = ObjectManager.GetEntitiesParallel<Tower>().Where(unit => this.Owner.IsValidOrbwalkingTarget(unit)).OfType<Unit>();

            return tower;
        }

        private void Hero_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            if (sender != this.Owner)
            {
                return;
            }

            if (!args.PropertyName.Equals("m_networkactivity", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var newNetworkActivity = (NetworkActivity)args.NewValue;

            if (this.attackActivityList.Contains(newNetworkActivity))
            {
                var diff = Game.RawGameTime - this.LastAttackTime;
                this.LastAttackTime = Game.RawGameTime - (Game.Ping / 2000f);
            }
        }

        private bool ShouldWait()
        {
            var t = 2f;
            return
                CreepManager.Instance()
                            .GetCreeps()
                            .Any(
                                unit =>
                                    unit.Team != this.Owner.Team && this.Owner.IsValidOrbwalkingTarget(unit)
                                    && HealthPredictionOLD.Instance().GetPredictedHealth(unit, t / this.Owner.AttacksPerSecond) < this.Owner.GetAttackDamage(unit, true));
        }
    }
}