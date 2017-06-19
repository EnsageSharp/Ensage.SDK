// <copyright file="IParticleManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Particle
{
    using System;

    using SharpDX;

    public interface IParticleManager : IDisposable
    {
        void AddOrUpdate(Entity unit, string name, string file, ParticleAttachment attachment, bool restart = true, params object[] controlPoints);

        void DrawCircle(Vector3 center, string id, float range, Color color);

        void DrawDangerLine(Unit unit, string id, Vector3 endPosition);

        void DrawTargetLine(Unit unit, string id, Vector3 endPosition);

        void DrawLine(Unit unit, string id, Vector3 endPosition, bool red = true);

        void DrawRange(Unit unit, string id, float range, Color color);

        bool HasParticle(string name);

        void Remove(string name);

        void ShowClick(Unit unit, string id, Vector3 position, Color color);
    }
}