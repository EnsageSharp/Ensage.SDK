// <copyright file="EntityManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Service;

    [Export(typeof(EntityManager<>))]
    public class EntityManager<TEntity> : IDisposable
        where TEntity : Entity, new()
    {
        private bool disposed;

        public EntityManager(IServiceContext context)
        {
            this.Context = context;
            Game.OnIngameUpdate += this.OnUpdate;
        }

        public IServiceContext Context { get; }

        public float Range { get; set; } = 3000;

        public float UpdateRate { get; set; } = 250;

        private TEntity[] Cache { get; set; }

        private float LastUpdateTime { get; set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<TEntity> GetEntries()
        {
            return this.Cache;
        }

        public ParallelQuery<TEntity> GetEntriesParallel()
        {
            return this.Cache.AsParallel();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                Game.OnIngameUpdate -= this.OnUpdate;
            }

            this.disposed = true;
        }

        protected virtual void OnUpdate(EventArgs args)
        {
            if (this.UpdateRate > 0)
            {
                var now = Game.RawGameTime;
                if ((now - this.LastUpdateTime) < (this.UpdateRate / 1000))
                {
                    return;
                }

                this.LastUpdateTime = now;
            }

            var position = this.Context.Owner.Position;

            this.Cache = ObjectManager.GetEntitiesParallel<TEntity>()
                                      .Where(e => e.IsValid && position.IsInRange(e, this.Range))
                                      .ToArray();
        }
    }
}