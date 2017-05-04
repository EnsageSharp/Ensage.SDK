// <copyright file="FocusTargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Input;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    using MouseEventArgs = Ensage.SDK.Input.MouseEventArgs;

    [ExportTargetSelector("Focus Target")]
    public class FocusTargetSelector : ITargetSelector, IDisposable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        [ImportingConstructor]
        public FocusTargetSelector([Import] IServiceContext context, [Import] IInputManager input, [Import] IParticleManager particle)
        {
            this.Owner = context.Owner;
            this.Input = input;
            this.Particle = particle;
        }

        private IInputManager Input { get; }

        private Hero Owner { get; }

        private IParticleManager Particle { get; }

        private Unit[] Targets { get; set; }

        public void Activate()
        {
            this.Input.MouseClick += this.OnMouseClick;
        }

        public void Deactivate()
        {
            this.Input.MouseClick -= this.OnMouseClick;
            this.Particle.Remove("FocusTarget");
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Unit> GetTargets()
        {
            return this.Targets?.Where(e => e.IsValid);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Input.MouseClick -= this.OnMouseClick;
            }

            this.disposed = true;
        }

        private void OnMouseClick(object sender, MouseEventArgs args)
        {
            if (args.Buttons != MouseButtons.Left && args.Buttons != MouseButtons.Right)
            {
                return;
            }

            var targets = EntityManager<Hero>
                .Entities
                .Where(e => e.IsAlive && !e.IsIllusion && e.Team != this.Owner.Team)
                .Where(e => e.Position.Distance(Game.MousePosition) < 400)
                .OrderBy(e => e.Position.Distance(Game.MousePosition));

            var target = targets.FirstOrDefault();
            if (target != null)
            {
                Log.Debug($"Update Focus {target.Name}");

                this.Targets = new Unit[] { target };
                this.Particle.DrawRange(target, "FocusTarget", target.HullRadius * 4, Color.Yellow);
            }
            else
            {
                this.Targets = new Unit[0];
                this.Particle.Remove("FocusTarget");
            }
        }
    }
}