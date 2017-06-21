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
    public class FocusTargetSelector : ControllableService, ITargetSelector
    {
        [ImportingConstructor]
        public FocusTargetSelector([Import] IServiceContext context, [Import] Lazy<IInputManager> input)
        {
            this.Owner = context.Owner;
            this.Input = input;
        }

        private Lazy<IInputManager> Input { get; }

        private Unit Owner { get; }

        private Unit[] Targets { get; set; }

        public IEnumerable<Unit> GetTargets()
        {
            return this.Targets?.Where(e => e.IsValid);
        }

        protected override void OnActivate()
        {
            this.Input.Value.MouseClick += this.OnMouseClick;
        }

        protected override void OnDeactivate()
        {
            this.Input.Value.MouseClick -= this.OnMouseClick;
        }

        private void OnMouseClick(object sender, MouseEventArgs args)
        {
            if (args.Buttons != MouseButtons.Left && args.Buttons != MouseButtons.Right)
            {
                return;
            }

            this.Targets = EntityManager<Hero>.Entities.Where(e => e.IsVisible && e.IsAlive && !e.IsIllusion && e.Team != this.Owner.Team)
                                              .Where(e => e.Position.Distance(Game.MousePosition) < 400)
                                              .OrderBy(e => e.Position.Distance(Game.MousePosition))
                                              .ToArray();
        }
    }
}