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
        private static readonly FrameCache<HashSet<Entity>> Cache;

        static EntityManager()
        {
            Cache = new FrameCache<HashSet<Entity>>(GetEntities);
        }

        public static event EventHandler FrameChanged
        {
            add
            {
                Cache.FrameChanged += value;
            }

            remove
            {
                Cache.FrameChanged -= value;
            }
        }

        internal static HashSet<Entity> Entities
        {
            get
            {
                return Cache.Value;
            }
        }

        private static HashSet<Entity> GetEntities()
        {
            return ObjectManager.GetEntities<Entity>().ToHashSet();
        }
    }

    public class EntityManager<T>
        where T : Entity, new()
    {
        private static HashSet<T> entities;

        static EntityManager()
        {
            EntityManager.FrameChanged += OnFrameChanged;
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

        private static void OnFrameChanged(object sender, EventArgs args)
        {
            entities = null;
        }
    }
}