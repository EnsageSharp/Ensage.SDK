// <copyright file="EntityManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;

    using System.Linq;

    using Ensage.SDK.Extensions;

    internal static class EntityManager
    {
        private static HashSet<Entity> entities;

        static EntityManager()
        {
            UpdateManager.SubscribeService(OnPreUpdate);
        }

        internal static event EventHandler PreUpdate;

        internal static HashSet<Entity> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = ObjectManager.GetEntities<Entity>().ToHashSet();
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
        private static HashSet<T> entities;

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
                    entities = EntityManager.Entities.OfType<T>().ToHashSet();
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