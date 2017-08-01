// <copyright file="IEntityContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    public interface IEntityContext<out T>
        where T : Entity
    {
        T Owner { get; }
    }
}