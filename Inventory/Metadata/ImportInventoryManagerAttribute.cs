// <copyright file="ImportInventoryManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ImportInventoryManagerAttribute : ImportAttribute
    {
        public ImportInventoryManagerAttribute()
            : base(typeof(IInventoryManager))
        {
        }
    }
}