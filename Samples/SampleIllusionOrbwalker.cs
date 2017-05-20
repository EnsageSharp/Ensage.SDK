// <copyright file="SampleIllusionOrbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin("SampleIllusionOrbwalker", StartupMode.Manual)]
    public class SampleIllusionOrbwalker : Plugin
    {
        [ImportingConstructor]
        public SampleIllusionOrbwalker(
            [Import] IServiceContext context)
        {
            this.Owner = context.Owner;
        }

        private Dictionary<Unit, IServiceContext> Orbwalkers { get; } = new Dictionary<Unit, IServiceContext>();

        private Unit Owner { get; }

        protected override void OnActivate()
        {
            UpdateManager.Subscribe(this.OnIllusionUpdate, 500);
            UpdateManager.Subscribe(this.OnUpdate, 100);
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.OnIllusionUpdate);
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        private void OnIllusionUpdate()
        {
            // find illusions
            var illusions = EntityManager<Unit>.Entities.Where(e => e.IsIllusion && e.IsAlive && e.Owner == this.Owner);

            foreach (var illusion in illusions)
            {
                if (this.Orbwalkers.ContainsKey(illusion))
                {
                    continue;
                }

                // create Illusion Context and Orbwalker
                this.Orbwalkers[illusion] = new EnsageServiceContext(illusion);
                this.Orbwalkers[illusion].GetExport<IOrbwalker>().Value.Activate();
            }

            foreach (var orbwalker in this.Orbwalkers.Where(e => !e.Key.IsValid || !e.Key.IsAlive).ToArray())
            {
                // remove dead illusions
                this.Orbwalkers.Remove(orbwalker.Key);
                orbwalker.Value.Dispose();
            }
        }

        private void OnUpdate()
        {
            if (Game.IsPaused)
            {
                return;
            }

            var target = EntityManager<Unit>.Entities.Where(e => e.IsAlive && e.Team != this.Owner.Team && e.Distance2D(Game.MousePosition) < 400)
                                            .OrderBy(e => e.Distance2D(Game.MousePosition))
                                            .FirstOrDefault();

            foreach (var context in this.Orbwalkers.Values.Where(e => e.Owner.IsValid))
            {
                var owner = context.Owner;
                if (!owner.IsAlive)
                {
                    continue;
                }

                context.Container.Get<IOrbwalker>().OrbwalkTo(target);
            }
        }
    }
}