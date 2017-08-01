// <copyright file="EnsageServiceContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Abilities;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Input;
    using Ensage.SDK.Inventory;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Prediction;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.TargetSelector;

    public sealed class EnsageServiceContext : IServiceContext
    {
        private bool disposed;

        public EnsageServiceContext(Unit unit)
        {
            if (unit == null || !unit.IsValid)
            {
                throw new ArgumentNullException(nameof(unit), "Unit is null or invalid");
            }

            this.Name = unit.Name;
            this.Owner = unit;
            this.Config = new SDKConfig();
            this.EntityContext = new EntityContext<Unit>(unit);
            this.Container = ContainerFactory.CreateContainer(this);
        }

        [Import]
        public Lazy<IAbilityDetector> AbilityDetector { get; private set; }

        [Import]
        public Lazy<AbilityFactory> AbilityFactory { get; private set; }

        public SDKConfig Config { get; }

        public ContextContainer<IServiceContext> Container { get; }

        public IEntityContext<Unit> EntityContext { get; }

        [Import]
        public Lazy<IInputManager> Input { get; private set; }

        [Import]
        public Lazy<IInventoryManager> Inventory { get; private set; }

        public string Name { get; }

        [Import]
        public Lazy<IOrbwalkerManager> Orbwalker { get; private set; }

        public Unit Owner { get; }

        [Import]
        public Lazy<IParticleManager> Particle { get; private set; }

        [Import]
        public Lazy<IPredictionManager> Prediction { get; private set; }

        [Import]
        public Lazy<IRendererManager> Renderer { get; private set; }

        [Import]
        public Lazy<ITargetSelectorManager> TargetSelector { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Equals(Unit other)
        {
            return this.Owner.Equals(other);
        }

        public T Get<T>(string contract = null)
        {
            return this.Container.Get<T>(contract);
        }

        public IEnumerable<T> GetAll<T>(string contract = null)
        {
            return this.Container.GetAll<T>(contract);
        }

        public Lazy<T> GetExport<T>(string contract = null)
        {
            return this.Container.GetExport<T>(contract);
        }

        public IEnumerable<Lazy<T, TMetadata>> GetExports<T, TMetadata>(string contract = null)
        {
            return this.Container.GetExports<T, TMetadata>(contract);
        }

        public override string ToString()
        {
            return $"Context({this.Name})";
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Container.Dispose();
            }

            this.disposed = true;
        }
    }
}