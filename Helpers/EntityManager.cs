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
        private static HashSet<Entity> cache = new HashSet<Entity>();

        static EntityManager()
        {
            OnRefresh();

            ObjectManager.OnAddEntity += OnAddEntity;
            ObjectManager.OnRemoveEntity += OnRemoveEntity;

            UpdateManager.SubscribeService(OnRefresh, 1000);
        }

        internal static HashSet<Entity> Entities
        {
            get
            {
                return cache;
            }
        }

        internal static HashSet<Entity> GetEntities()
        {
            return ObjectManager.GetEntities<Entity>().Concat(ObjectManager.GetDormantEntities<Entity>()).ToHashSet();
        }

        private static void OnAddEntity(EntityEventArgs args)
        {
            cache.Add(args.Entity);
        }

        private static void OnRefresh()
        {
            cache = GetEntities();
        }

        private static void OnRemoveEntity(EntityEventArgs args)
        {
            cache.Remove(args.Entity);
        }
    }

    public class EntityManager<T>
        where T : Entity, new()
    {
        private static HashSet<T> cache = new HashSet<T>();

        static EntityManager()
        {
            OnRefresh();

            ObjectManager.OnAddEntity += OnAddEntity;
            ObjectManager.OnRemoveEntity += OnRemoveEntity;

            UpdateManager.SubscribeService(OnRefresh, 1000);
        }

        public static event EventHandler<T> EntityAdded;

        public static event EventHandler<T> EntityRemoved;

        public static IEnumerable<T> Entities
        {
            get
            {
                return cache.Where(x => x.IsValid);
            }
        }

        public override string ToString()
        {
            return $"EntityManager<{typeof(T).Name}>";
        }

        internal static HashSet<T> GetEntities()
        {
            return EntityManager.Entities.OfType<T>().ToHashSet();
        }

        private static void OnAddEntity(EntityEventArgs args)
        {
            var type = args.Entity as T;
            if (type != null)
            {
                cache.Add(type);
                EntityAdded?.Invoke(null, type);
            }
        }

        private static void OnRefresh()
        {
            cache = GetEntities();
        }

        private static void OnRemoveEntity(EntityEventArgs args)
        {
            var type = args.Entity as T;
            if (type != null)
            {
                cache.Remove(type);
                EntityRemoved?.Invoke(null, type);
            }
        }
    }
}