// <copyright file="LowestHealthSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    using SharpDX;

    [ExportTargetSelector("Lowest Health")]
    public class LowestHealthSelector : SelectorBase
    {
        [ImportingConstructor]
        public LowestHealthSelector([Import] IServiceContext context, [Import] IParticleManager particle)
            : base(context)
        {
            this.Particle = particle;
        }

        private IParticleManager Particle { get; }

        public override void Deactivate()
        {
            this.Particle.Remove("LowestHealth");
        }

        public override IEnumerable<Unit> GetTargets()
        {
            if (this.Targets == null)
            {
                var team = this.Owner.Team;

                this.Targets = EntityManager<Hero>
                    .Entities
                    .Where(e => e.IsAlive && !e.IsIllusion && e.Team != team && e.Health > 0)
                    .OrderBy(e => e.Health)
                    .ToArray();

                var target = this.Targets.FirstOrDefault();
                if (target != null)
                {
                    this.Particle.DrawRange(target, "LowestHealth", target.HullRadius * 4, Color.Yellow);
                }
                else
                {
                    this.Particle.Remove("LowestHealth");
                }
            }

            return this.Targets;
        }
    }
}