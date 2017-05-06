// <copyright file="FocusTargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Forms;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Input;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    using MouseEventArgs = Ensage.SDK.Input.MouseEventArgs;

    [ExportTargetSelector("Focus Target")]
    public class FocusTargetSelector : ITargetSelector, IDisposable
    {
        private bool disposed;

        [ImportingConstructor]
        public FocusTargetSelector([Import] IServiceContext context, [Import] IInputManager input)
        {
            this.Owner = context.Owner;
            this.Input = input;
        }

        private IInputManager Input { get; }

        private Hero Owner { get; }

        private Unit[] Targets { get; set; }

        public void Activate()
        {
            this.Input.MouseClick += this.OnMouseClick;
        }

        public void Deactivate()
        {
            this.Input.MouseClick -= this.OnMouseClick;
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

            this.Targets = EntityManager<Hero>.Entities.Where(e => e.IsAlive && !e.IsIllusion && e.Team != this.Owner.Team)
                                              .Where(e => e.Position.Distance(Game.MousePosition) < 400).OrderBy(e => e.Position.Distance(Game.MousePosition)).ToArray();
        }
    }
}