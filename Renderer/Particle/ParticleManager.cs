// <copyright file="ParticleManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Particle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Renderer.Particle.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportParticleManager]
    public class ParticleManager : IParticleManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly List<ParticleEffectContainer> Particles = new List<ParticleEffectContainer>();

        private bool disposed;

        public void AddOrUpdate(Entity unit, string name, string file, ParticleAttachment attachment, params object[] controlPoints)
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
                    particle.SetControlPoints(controlPoints);
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
            this.AddOrUpdate(ObjectManager.LocalHero, id, "particles/ui_mouseactions/drag_selected_ring.vpcf", ParticleAttachment.AbsOrigin, 0, center, 1, color, 2, range * 1.1f);
        }

        /// <summary>
        ///     Draws a red line from unit to endPosition
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="id"></param>
        /// <param name="endPosition"></param>
        public void DrawDangerLine(Unit unit, string id, Vector3 endPosition)
        {
            this.AddOrUpdate(unit, id, "particles/ui_mouseactions/range_finder_tower_line.vpcf", ParticleAttachment.AbsOriginFollow, 6, true, 2, endPosition, 7, unit.Position);
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

            this.AddOrUpdate(unit, id, "particles/ui_mouseactions/range_finder_line.vpcf", ParticleAttachment.AbsOrigin, 0, startPos, 1, pos1, 2, endPosition);
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
            this.AddOrUpdate(unit, id, "particles/ui_mouseactions/drag_selected_ring.vpcf", ParticleAttachment.AbsOriginFollow, 1, color, 2, range * 1.1f);
        }

        public bool HasParticle(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Particles.Any(p => p.Name == name);
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
            this.AddOrUpdate(unit, id, "particles/ui_mouseactions/clicked_basemove.vpcf", ParticleAttachment.AbsOrigin, 0, position, 1, new Vector3(color.R, color.G, color.B));
        }

        protected virtual void Dispose(bool disposing)
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