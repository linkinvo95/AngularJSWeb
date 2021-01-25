using System;
using Common;
using SimpleInjector;

namespace Core
{
    public static class CoreConfiguration
    {
        public static void Initialize(Container container, Lifestyle lifestyle)
        {
            container.RegisterSingleton<IServiceProvider>(() => container);

            var assembly = typeof(CoreConfiguration).Assembly;
            InitializeAssemblyInstancesService.Initialize(container, lifestyle, assembly);
        }
    }
}