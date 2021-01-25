using System;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace Common
{
    public static class InitializeAssemblyInstancesService
    {
        private static Lifestyle GetLifestyle(AutoRegisterAttribute attribute, Lifestyle lifestyle)
        {
            switch (attribute.Type)
            {
                case AutoRegisterTypes.Singleton:
                    return Lifestyle.Singleton;
                default:
                    return lifestyle;
            }
        }

        public static void RegisterAssemblyWithSerializableTypes(Container container, Assembly assembly)
        {
            container.RegisterInitializer<ListTypeLookup<Assembly>>(c => { c.Register(assembly); });
        }

        public static void Initialize(Container container, Lifestyle lifestyle, Assembly assembly)
        {
            var entities = (from type in assembly.GetTypes()
                let attr = type.GetCustomAttributes(typeof(AutoRegisterAttribute), true)
                where attr.Length == 1
                select new {Type = type, Attribute = attr.First() as AutoRegisterAttribute}).ToList();

            foreach (var entity in entities)
            {
                var simpleInjectionLifestyle = GetLifestyle(entity.Attribute, lifestyle);
                var type = entity.Type;

                var allInterfaces = type.GetInterfaces();
                var interfaces = allInterfaces.Where(q => !allInterfaces.Any(x => x.GetInterfaces().Contains(q))).ToList();
                if (type.BaseType != null)
                {
                    interfaces.RemoveAll(q => type.BaseType.GetInterfaces().Contains(q));
                }

                if (interfaces.Count == 1)
                {
                    var serviceType = interfaces.First();

                    if (type.IsGenericType)
                    {
                        container.Register(serviceType.GetGenericTypeDefinition(), type.GetGenericTypeDefinition(), simpleInjectionLifestyle);
                    }
                    else
                    {
                        container.Register(serviceType, type, simpleInjectionLifestyle);
                    }
                }
                else
                {
                    if (type.IsGenericType)
                    {
                        throw new ApplicationException("Add support to multiple generic type registration.");
                    }
                    var groupedRegistration = simpleInjectionLifestyle.CreateRegistration(type, container);
                    foreach (var @interface in interfaces)
                    {
                        container.AddRegistration(@interface, groupedRegistration);
                    }
                }
            }
        }
    }
}