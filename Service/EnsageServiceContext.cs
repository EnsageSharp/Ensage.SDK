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
    using Ensage.SDK.Menu;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Prediction;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.TargetSelector;

    public sealed class EnsageServiceContext : IServiceContext
    {
        [Import]
        private Lazy<IAbilityDetector> abilityDetector;

        [Import]
        private Lazy<AbilityFactory> abilityFactory;

        private bool disposed;

        [Import]
        private Lazy<IInputManager> inputManager;

        [Import]
        private Lazy<IInventoryManager> inventoryManager;

        [Import]
        private Lazy<IOrbwalkerManager> orbwalkerManager;

        [Import]
        private Lazy<IParticleManager> particleManager;

        [Import]
        private Lazy<IPredictionManager> predictionManager;

        [Import]
        internal Lazy<IRenderManager> rendererManager;

        [Import]
        private Lazy<ITargetSelectorManager> targetSelectorManager;

        [Import]
        internal Lazy<MenuManager> menuManager;

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

        public IAbilityDetector AbilityDetector
        {
            get
            {
                return this.abilityDetector.Value;
            }
        }

        public AbilityFactory AbilityFactory
        {
            get
            {
                return this.abilityFactory.Value;
            }
        }

        public MenuManager MenuManager
        {
            get
            {
                return this.menuManager.Value;
            }
        }

        public SDKConfig Config { get; }

        public ContextContainer<IServiceContext> Container { get; }

        public IEntityContext<Unit> EntityContext { get; }

        public IInputManager Input
        {
            get
            {
                return this.inputManager.Value;
            }
        }

        public IInventoryManager Inventory
        {
            get
            {
                return this.inventoryManager.Value;
            }
        }

        public string Name { get; }

        public IOrbwalkerManager Orbwalker
        {
            get
            {
                return this.orbwalkerManager.Value;
            }
        }

        public Unit Owner { get; }

        public IParticleManager Particle
        {
            get
            {
                return this.particleManager.Value;
            }
        }

        public IPredictionManager Prediction
        {
            get
            {
                return this.predictionManager.Value;
            }
        }

        public IRenderManager RenderManager
        {
            get
            {
                return this.rendererManager.Value;
            }
        }

        [Obsolete("Use RenderManager")]
        public IRenderManager Renderer
        {
            get
            {
                return this.rendererManager.Value;
            }
        }

        public ITextureManager TextureManager
        {
            get
            {
                return this.rendererManager.Value.TextureManager;
            }
        }

        public ITargetSelectorManager TargetSelector
        {
            get
            {
                return this.targetSelectorManager.Value;
            }
        }

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