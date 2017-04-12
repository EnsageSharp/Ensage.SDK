// <copyright file="CreepManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Extensions;

    class CreepManager
    {
        private static CreepManager _Instance;

        private static List<Creep> Creeps = new List<Creep>();

        private static float LastUpdateTime = 0f;

        public CreepManager()
        {
            Game.OnIngameUpdate += this.GameDispatcherOnOnIngameUpdate;
        }

        public Hero Hero
        {
            get
            {
                return ObjectManager.LocalHero;
            }
        }

        public static CreepManager Instance()
        {
            if (_Instance == null)
            {
                _Instance = new CreepManager();
            }

            return _Instance;
        }

        public ParallelQuery<Unit> GetCreeps()
        {
            return Creeps.AsParallel().Where(unit => unit.IsValid).OfType<Unit>();
        }

        private void GameDispatcherOnOnIngameUpdate(EventArgs args)
        {
            var now = Game.RawGameTime;
            if ((now - LastUpdateTime) < 0.25f)
            {
                return;
            }

            LastUpdateTime = now;
            var heroPosition = this.Hero.Position;
            Creeps =
                ObjectManager.GetEntitiesParallel<Creep>()
                             .Where(
                                 unit =>
                                     unit.IsValid && unit.IsAlive && unit.IsSpawned
                                     && heroPosition.IsInRange(unit, 3000f))
                             .ToList();
        }
    }
}