// <copyright file="CollisionTypes.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction.Collision
{
    using System;

    [Flags]
    public enum CollisionTypes
    {
        None = 1,

        Trees = 2,

        Buildings = 4,

        Terrain = 8,

        AllyCreeps = 16,

        EnemyCreeps = 32,

        AllyHeroes = 64,

        EnemyHeroes = 128
    }
}