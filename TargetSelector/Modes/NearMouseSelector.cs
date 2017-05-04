// <copyright file="NearMouseSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.Common.Menu;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    using SharpDX;

    [ExportTargetSelector("Near Mouse")]
    public class NearMouseSelector : SelectorBase
    {
        [ImportingConstructor]
        public NearMouseSelector([Import] IServiceContext context, [Import] IParticleManager particle, [Import] TargetSelectorConfig parent)
            : base(context)
        {
            this.Particle = particle;
            this.Config = new NearMouseConfig(parent.Factory);
        }

        public NearMouseConfig Config { get; }

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
                    .Where(e => e.Position.Distance(pos) < this.Config.Range.Value.Value)
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

    public class NearMouseConfig
    {
        public NearMouseConfig(MenuFactory parent)
        {
            this.Factory = parent.Menu("Near Mouse");
            this.Range = this.Factory.Item("Range", new Slider(800, 100, 2000));
        }

        public MenuFactory Factory { get; }

        public MenuItem<Slider> Range { get; }
    }
}