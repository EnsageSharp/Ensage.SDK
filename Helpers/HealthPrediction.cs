// <copyright file="HealthPrediction.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.ComponentModel.Composition;

    [Export(typeof(HealthPrediction<>))]
    public class HealthPrediction<TEntity> : IDisposable
        where TEntity : Entity, new()
    {
        private bool disposed;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public float GetPredictedHealth(Unit unit, float untilTime)
        {
            return 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: managed
            }

            // TODO: unmanaged
            this.disposed = true;
        }
    }
}