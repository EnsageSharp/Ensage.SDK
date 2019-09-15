// <copyright file="ParticleManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Particle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Renderer.Particle.Metadata;

    using NLog;

    using SharpDX;

    [ExportParticleManager]
    public sealed class ParticleManager : IParticleManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private static readonly List<ParticleEffectContainer> Particles = new List<ParticleEffectContainer>();

        private bool disposed;

        public void AddOrUpdate(Entity unit, string name, string file, ParticleAttachment attachment, RestartType restart = RestartType.FullRestart, params object[] controlPoints)
        {
            if (unit == null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            if (!unit.IsValid)
            {
                throw new ArgumentException("Value should be valid.", nameof(unit));
            }

            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var particle = Particles.FirstOrDefault(p => p.Name == name);

            if (particle == null)
            {
                Particles.Add(new ParticleEffectContainer(name, file, unit, attachment, controlPoints));
            }
            else
            {
                // parts changed
                if (particle.Unit != unit || particle.File != file || particle.Attachment != attachment)
                {
                    particle.Dispose();

                    Particles.Remove(particle);
                    Particles.Add(new ParticleEffectContainer(name, file, unit, attachment, controlPoints));
                    return;
                }

                // control points changed
                // var hash = controlPoints.Sum(p => p.GetHashCode());
                var hash = controlPoints.Aggregate(0, (sum, p) => unchecked(sum + p.GetHashCode()));
                if (particle.GetControlPointsHashCode() != hash)
                {
                    particle.SetControlPoints(restart, controlPoints);
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Draws a range indicator around the point
        /// </summary>
        /// <param name="center"></param>
        /// <param name="id"></param>
        /// <param name="range"></param>
        /// <param name="color"></param>
        public void DrawCircle(Vector3 center, string id, float range, Color color)
        {
            this.AddOrUpdate(
                ObjectManager.LocalHero,
                id,
                "particles/ui_mouseactions/drag_selected_ring.vpcf",
                ParticleAttachment.AbsOrigin,
                RestartType.FullRestart,
                0,
                center,
                1,
                color,
                2,
                range * 1.1f);
        }

        /// <summary>
        ///     Draws a line from unit to endPosition
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="id"></param>
        /// <param name="endPosition"></param>
        /// <param name="color"></param>
        public void DrawDangerLine(Unit unit, string id, Vector3 endPosition, Color? color = null)
        {
            this.AddOrUpdate(
                unit,
                id,
                "materials/ensage_ui/particles/target_d.vpcf",
                ParticleAttachment.AbsOriginFollow,
                RestartType.None,
                5,
                color ?? Color.Red,
                6,
                new Vector3(255), // alpha
                2,
                unit.Position,
                7,
                endPosition);
        }

        /// <summary>
        ///     Draws a line from unit to endPosition
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="id"></param>
        /// <param name="endPosition"></param>
        /// <param name="red"></param>
        public void DrawLine(Unit unit, string id, Vector3 endPosition, bool red = true)
        {
            var startPos = unit.Position;
            var pos1 = !red ? startPos : endPosition;

            this.AddOrUpdate(
                unit,
                id,
                "particles/ui_mouseactions/range_finder_line.vpcf",
                ParticleAttachment.AbsOrigin,
                RestartType.FullRestart,
                0,
                startPos,
                1,
                pos1,
                2,
                endPosition);
        }

        /// <summary>
        ///     Draws a range indicator around the unit
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="id"></param>
        /// <param name="range"></param>
        /// <param name="color"></param>
        public void DrawRange(Unit unit, string id, float range, Color color)
        {
            this.AddOrUpdate(
                unit,
                id,
                "particles/ui_mouseactions/drag_selected_ring.vpcf",
                ParticleAttachment.AbsOriginFollow,
                RestartType.FullRestart,
                1,
                color,
                2,
                range * -1.1f);
        }

        /// <summary>
        ///     Draws a line from unit to endPosition with a circle
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="id"></param>
        /// <param name="endPosition"></param>
        /// <param name="color"></param>
        public void DrawTargetLine(Unit unit, string id, Vector3 endPosition, Color? color = null)
        {
            this.AddOrUpdate(
                unit,
                id,
                "materials/ensage_ui/particles/target.vpcf",
                ParticleAttachment.AbsOriginFollow,
                RestartType.None,
                5,
                color ?? Color.Red,
                6,
                new Vector3(255), // alpha
                2,
                unit.Position,
                7,
                endPosition);
        }

        public bool HasParticle(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Particles.Any(p => p.Name == name);
        }

        public void Release(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var particle = Particles.FirstOrDefault(p => p.Name == name);
            if (particle != null)
            {
                particle.Release();
            }
        }

        public void Remove(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            foreach (var particle in Particles.Where(p => p.Name == name).ToArray())
            {
                Particles.Remove(particle);
                particle.Dispose();
            }
        }

        /// <summary>
        ///     Shows the click effect on position
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="id"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void ShowClick(Unit unit, string id, Vector3 position, Color color)
        {
            this.AddOrUpdate(
                unit,
                id,
                "particles/ui_mouseactions/clicked_basemove.vpcf",
                ParticleAttachment.AbsOrigin,
                RestartType.FullRestart,
                0,
                position,
                1,
                new Vector3(color.R, color.G, color.B));
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var particle in Particles)
                {
                    try
                    {
                        particle?.Dispose();
                    }
                    catch (Exception e)
                    {
                        Log.Warn(e);
                    }
                }
            }

            this.disposed = true;
        }
    }
}