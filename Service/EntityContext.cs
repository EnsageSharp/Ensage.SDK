// <copyright file="EntityContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class EntityContext<T> : IEntityContext<T>
        where T : Entity
    {
        public EntityContext([NotNull] T owner)
        {
            if (owner == null || !owner.IsValid)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            this.Owner = owner;
        }

        public T Owner { get; }

        public static implicit operator T(EntityContext<T> context)
        {
            return context.Owner;
        }
    }
}