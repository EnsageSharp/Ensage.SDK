// <copyright file="TableExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Collections.Generic;

    using Ensage.SDK.Helpers;

    public static class TableExtensions
    {
        public static TableColumn ToColumn(this IEnumerable<string> data, string name, int minSize = 0)
        {
            return new TableColumn(name, minSize, data);
        }
    }
}