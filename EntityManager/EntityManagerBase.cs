// <copyright file="EntityManagerBase.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EntityManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Helpers;

    public abstract class EntityManagerBase
    {
        static EntityManagerBase()
        {
            Handler = new ReflectionEventHandler<EventArgs>(typeof(Game), "PreUpdate");
            Handler.Attach(OnPreUpdate);
        }

        protected static event EventHandler PreUpdate;

        private static IReadOnlyCollection<Entity> Entities { get; set; } = new Entity[0];

        private static ReflectionEventHandler<EventArgs> Handler { get; }

        protected static IEnumerable<Entity> GetEntities()
        {
            if (Entities == null)
            {
                Entities = ObjectManager.GetEntities<Entity>().ToArray();
            }

            return Entities;
        }

        private static void OnPreUpdate(EventArgs args)
        {
            Entities = null;
            PreUpdate?.Invoke(null, args);
        }
    }
}