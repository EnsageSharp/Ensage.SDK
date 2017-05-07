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

        static EntityManager()
        {
            Refresh();
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
    }

    public class EntityManager<T>
        where T : Entity, new()
    {
        private static HashSet<T> cache = new HashSet<T>();

        static EntityManager()
        {
            Refresh();
        }

        public static HashSet<T> Entities
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
            cache = EntityManager.Entities.OfType<T>().ToHashSet();
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
    }
}