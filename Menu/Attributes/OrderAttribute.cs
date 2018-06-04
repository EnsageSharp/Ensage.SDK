// <copyright file="OrderAttribute.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class OrderAttribute : Attribute
    {
        public OrderAttribute(int orderNumber)
        {
            this.OrderNumber = orderNumber - 1;
        }

        public int OrderNumber { get; }
    }
}
