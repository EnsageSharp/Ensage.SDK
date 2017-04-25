// <copyright file="EntityManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EntityManager
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;
    using System.Linq;

    [Export(typeof(EntityManager<>))]
    public class EntityManager<T> : EntityManagerBase, INotifyCollectionChanged
        where T : Entity, new()
    {
        public EntityManager()
        {
            EntityManagerBase.PreUpdate += this.OnPreUpdate;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private IReadOnlyCollection<T> Entities { get; set; }

        public virtual IEnumerable<T> Get()
        {
            if (this.Entities == null)
            {
                this.Entities = GetEntities().OfType<T>().ToArray();
            }

            return this.Entities.Where(e => e.IsValid);
        }

        private void OnPreUpdate(object sender, EventArgs args)
        {
            this.Entities = null;
            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}