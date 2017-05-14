// <copyright file="TypeExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class TypeExtensions
    {
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> valueFactory)
        {
            TValue val;

            if (!dict.TryGetValue(key, out val))
            {
                val = valueFactory();
                dict.Add(key, val);
            }

            return val;
        }

        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;

                default:
                    return false;
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }
    }
}