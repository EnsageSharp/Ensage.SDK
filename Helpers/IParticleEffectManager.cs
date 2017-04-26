// <copyright file="IParticleEffectManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    using SharpDX;

    public interface IParticleEffectManager : IDisposable
    {
        void AddOrUpdate(Entity unit, string name, string file, ParticleAttachment attachment, params object[] controlPoints);

        void DrawCircle(Vector3 center, string id, float range, Color color);

        void DrawDangerLine(Unit unit, string id, Vector3 endPosition);

        void DrawLine(Unit unit, string id, Vector3 endPosition, bool red = true);

        void DrawRange(Unit unit, string id, float range, Color color);

        void ShowClick(Unit unit, string id, Vector3 position, Color color);
    }
}