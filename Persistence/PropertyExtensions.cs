// <copyright file="PropertyExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ExpressionExtensions
    {
        public static Func<object, object> GetPropertyGetter(this PropertyInfo property, object target)
        {
            var targetType = target.GetType();
            var getter = property.GetMethod;

            var targetParameter = Expression.Parameter(typeof(object), "target");
            var targetConvert = Expression.Convert(targetParameter, targetType);

            var call = Expression.Call(targetConvert, getter);
            var resultConvert = Expression.Convert(call, typeof(object));

            var action = Expression.Lambda<Func<object, object>>(resultConvert, targetParameter);

            return action.Compile();
        }

        public static Action<object, object> GetPropertySetter(this PropertyInfo property, object target)
        {
            var targetType = target.GetType();
            var propertyType = property.PropertyType;
            var setter = property.SetMethod;

            var targetParameter = Expression.Parameter(typeof(object), "target");
            var valueParameter = Expression.Parameter(typeof(object), "value");

            var targetConvert = Expression.Convert(targetParameter, targetType);
            var valueConvert = Expression.Convert(valueParameter, propertyType);

            var call = Expression.Call(targetConvert, setter, valueConvert);
            var action = Expression.Lambda<Action<object, object>>(call, targetParameter, valueParameter);

            return action.Compile();
        }
    }
}