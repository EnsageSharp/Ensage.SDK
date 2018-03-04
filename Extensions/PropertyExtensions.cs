// <copyright file="PropertyExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class PropertyExtensions
    {
        public static Func<object, TResult> GetPropertyGetter<TResult>(this PropertyInfo property, object target)
        {
            var targetType = target.GetType();
            var getter = property.GetMethod;

            var targetParameter = Expression.Parameter(typeof(object), "target");
            var targetConvert = Expression.Convert(targetParameter, targetType);

            var call = Expression.Call(targetConvert, getter);
            var resultConvert = Expression.Convert(call, typeof(TResult));

            var action = Expression.Lambda<Func<object, TResult>>(resultConvert, targetParameter);

            return action.Compile();
        }

        public static Action<object, TResult> GetPropertySetter<TResult>(this PropertyInfo property, object target)
        {
            var targetType = target.GetType();
            var propertyType = property.PropertyType;
            var setter = property.SetMethod;

            var targetParameter = Expression.Parameter(typeof(object), "target");
            var valueParameter = Expression.Parameter(typeof(TResult), "value");

            var targetConvert = Expression.Convert(targetParameter, targetType);
            var valueConvert = Expression.Convert(valueParameter, propertyType);

            var call = Expression.Call(targetConvert, setter, valueConvert);
            var action = Expression.Lambda<Action<object, TResult>>(call, targetParameter, valueParameter);

            return action.Compile();
        }
    }
}