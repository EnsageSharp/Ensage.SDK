// <copyright file="CollisionTypes.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction.Collision
{
    using System;

    [Flags]
    public enum CollisionTypes
    {
        None = 1 << 0,

        Trees = 1 << 1,

        Buildings = 1 << 2,

        Terrain = 1 << 3,

        AllyCreeps = 1 << 4,

        EnemyCreeps = 1 << 5,

        AllyHeroes = 1 << 6,

        EnemyHeroes = 1 << 7,

        Runes = 1 << 8,

        AllUnits = AllyCreeps | AllyHeroes | EnemyCreeps | EnemyHeroes,

        AlliedUnits = AllyCreeps | AllyHeroes,

        EnemyUnits = EnemyCreeps | EnemyHeroes,

        All = Trees | Buildings | Terrain | AllUnits,
    }
}