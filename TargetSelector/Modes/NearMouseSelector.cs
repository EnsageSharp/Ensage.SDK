// <copyright file="NearMouseSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    using SharpDX;

    [ExportTargetSelector("Near Mouse")]
    public class NearMouseSelector : SelectorBase
    {
        [ImportingConstructor]
        public NearMouseSelector([Import] IServiceContext context, [Import] IParticleManager particle)
            : base(context)
        {
            this.Particle = particle;
        }

        private IParticleManager Particle { get; }

        public override void Deactivate()
        {
            this.Particle.Remove("NearMouse");
        }

        public override IEnumerable<Unit> GetTargets()
        {
            if (this.Targets == null)
            {
                var pos = Game.MousePosition;
                var team = this.Owner.Team;

                this.Targets = EntityManager<Hero>
                    .Entities
                    .Where(e => e.IsAlive && !e.IsIllusion && e.Team != team)
                    .Where(e => e.Position.Distance(pos) < 600)
                    .OrderBy(e => e.Position.Distance(pos))
                    .ToImmutableList();

                var target = this.Targets.FirstOrDefault();
                if (target != null)
                {
                    this.Particle.DrawRange(target, "NearMouse", target.HullRadius * 4, Color.Yellow);
                }
                else
                {
                    this.Particle.Remove("NearMouse");
                }
            }

            return this.Targets;
        }
    }
}