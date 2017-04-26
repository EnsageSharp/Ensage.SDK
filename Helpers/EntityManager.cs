// <copyright file="EntityManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    internal static class EntityManager
    {
        private static ImmutableHashSet<Entity> entities;

        static EntityManager()
        {
            UpdateManager.SubscribeService(OnPreUpdate);
        }

        internal static event EventHandler PreUpdate;

        internal static ImmutableHashSet<Entity> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = ObjectManager.GetEntities<Entity>().ToImmutableHashSet();
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
        private static ImmutableHashSet<T> entities;

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
                    entities = EntityManager.Entities.OfType<T>().ToImmutableHashSet();
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