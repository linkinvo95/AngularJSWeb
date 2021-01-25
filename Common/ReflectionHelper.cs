using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common
{
    public static class ReflectionHelper
    {
        public static void InvokeMethod(object entity, string method, params object[] args)
        {
            var type = entity.GetType();
            var methodInfo = type.GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                throw new NotImplementedException($"The '{type.Name}.{method}' doesn't exist. Please contact support.");
            }
            methodInfo.Invoke(entity, args);
        }

        public static void SetProperty(object entity, PropertyInfo property, object value)
        {
            var type = entity.GetType();
            Set(entity, type, property, value);
        }

        public static void SetProperties(object entity, IDictionary<string, object> values)
        {
            var type = entity.GetType();
            foreach (var propertyInfo in type.GetProperties().Where(q => values.Keys.Contains(q.Name)))
            {
                Set(entity, type, propertyInfo, values[propertyInfo.Name]);
            }
        }

        private static void Set(object entity, Type type, PropertyInfo property, object value)
        {
            var propertyInfo = type.GetProperty(property.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            while (type != null && propertyInfo != null && propertyInfo.SetMethod == null)
            {
                if (type.BaseType == null)
                {
                    break;
                }
                type = type.BaseType;
                propertyInfo = type.GetProperty(property.Name);
            }

            if (type == null || propertyInfo == null || propertyInfo.SetMethod == null)
            {
                throw new NullReferenceException($"Type '{entity.GetType().Name}' doesn't have a set property '{property.Name}'.");
            }
            propertyInfo.SetValue(entity, value);
        }
    }
}