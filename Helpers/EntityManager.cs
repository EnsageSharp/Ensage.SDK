// <copyright file="EntityManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    public static class EntityManager
    {
        private static ImmutableSortedSet<Entity> entities;

        static EntityManager()
        {
            UpdateManager.SubscribeService(OnPreUpdate);
        }

        public static event EventHandler PreUpdate;

        public static IEnumerable<Entity> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = ObjectManager.GetEntities<Entity>().ToImmutableSortedSet();
                }

                return entities;
            }
        }

        private static void OnPreUpdate()
        {
            entities = null;
            PreUpdate?.Invoke(null, EventArgs.Empty);
        }
    }

    public class EntityManager<T>
        where T : Entity, new()
    {
        private static ImmutableSortedSet<T> entities;

        static EntityManager()
        {
            EntityManager.PreUpdate += OnPreUpdate;
        }

        public static IEnumerable<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = EntityManager.Entities.OfType<T>().ToImmutableSortedSet();
                }

                return entities.Where(e => e.IsValid);
            }
        }

        private static void OnPreUpdate(object sender, EventArgs args)
        {
            entities = null;
        }
    }
}