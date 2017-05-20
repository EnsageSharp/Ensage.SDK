// <copyright file="EnsageServiceContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;

    public sealed class EnsageServiceContext : IServiceContext
    {
        private bool disposed;

        public EnsageServiceContext(Unit unit)
        {
            if (unit == null || !unit.IsValid)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            this.Name = unit.Name;
            this.Owner = unit;
            this.Container = ContainerFactory.CreateContainer(this);
        }

        public ContextContainer<IServiceContext> Container { get; }

        public string Name { get; }

        public Unit Owner { get; }

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