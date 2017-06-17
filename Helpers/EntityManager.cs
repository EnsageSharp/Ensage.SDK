// <copyright file="EntityManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Extensions;

    internal static class EntityManager
    {
        private static HashSet<Entity> cache = new HashSet<Entity>();

        private static HashSet<Entity> dormantCache = new HashSet<Entity>();

        static EntityManager()
        {
            UpdateManager.SubscribeService(OnUpdate, 1000);
        }

        internal static HashSet<Entity> DormantEntities
        {
            get
            {
                return dormantCache;
            }
        }

        internal static HashSet<Entity> Entities
        {
            get
            {
                return cache;
            }
        }

        internal static void Refresh()
        {
            ObjectManager.OnAddEntity -= OnAddEntity;
            ObjectManager.OnRemoveEntity -= OnRemoveEntity;
            cache = ObjectManager.GetEntities<Entity>().ToHashSet();
            dormantCache = ObjectManager.GetDormantEntities<Entity>().ToHashSet();
            ObjectManager.OnAddEntity += OnAddEntity;
            ObjectManager.OnRemoveEntity += OnRemoveEntity;
        }

        private static void OnAddEntity(EntityEventArgs args)
        {
            cache.Add(args.Entity);
        }

        private static void OnRemoveEntity(EntityEventArgs args)
        {
            cache.Remove(args.Entity);
        }

        private static void OnUpdate()
        {
            Refresh();
        }
    }

    public class EntityManager<T>
        where T : Entity, new()
    {
        private static HashSet<T> cache = new HashSet<T>();

        private static HashSet<T> dormantCache = new HashSet<T>();

        static EntityManager()
        {
            UpdateManager.SubscribeService(OnUpdate, 1000);
        }

        public static HashSet<T> DormantEntities
        {
            get
            {
                return dormantCache;
            }
        }

        public static HashSet<T> Entities
        {
            get
            {
                return cache;
            }
        }

        public override string ToString()
        {
            return $"EntityManager<{typeof(T).Name}>";
        }

        internal static void Refresh()
        {
            ObjectManager.OnAddEntity -= OnAddEntity;
            ObjectManager.OnRemoveEntity -= OnRemoveEntity;
            cache = EntityManager.Entities.OfType<T>().ToHashSet();
            dormantCache = EntityManager.DormantEntities.OfType<T>().ToHashSet();
            ObjectManager.OnAddEntity += OnAddEntity;
            ObjectManager.OnRemoveEntity += OnRemoveEntity;
        }

        private static void OnAddEntity(EntityEventArgs args)
        {
            var type = args.Entity as T;
            if (type != null)
            {
                cache.Add(type);
            }
        }

        private static void OnRemoveEntity(EntityEventArgs args)
        {
            var type = args.Entity as T;
            if (type != null)
            {
                cache.Remove(type);
            }
        }

        private static void OnUpdate()
        {
            Refresh();
        }
    }
}