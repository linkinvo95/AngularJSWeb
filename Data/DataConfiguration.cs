using System.Reflection;
using BusinessEntities;
using Common;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Imports.Newtonsoft.Json;
using SimpleInjector;

namespace Data
{
    public class DataConfiguration
    {
        public static void Initialize(Container container, Lifestyle lifestyle, bool createIndexes = true)
        {
            var assembly = typeof(DataConfiguration).Assembly;

            container.RegisterSingleton<IListTypeLookup<Assembly>, ListTypeLookup<Assembly>>();

            InitializeAssemblyInstancesService.RegisterAssemblyWithSerializableTypes(container, typeof(User).Assembly);
            InitializeAssemblyInstancesService.RegisterAssemblyWithSerializableTypes(container, assembly);

            InitializeAssemblyInstancesService.Initialize(container, lifestyle, assembly);
            container.RegisterSingleton(() => InitializeDocumentStore(assembly, createIndexes));

            container.Register(() =>
            {
                var session = container.GetInstance<IDocumentStore>().OpenSession();
                session.Advanced.MaxNumberOfRequestsPerSession = 5000;
                return session;
            }, lifestyle);
        }

        private static IDocumentStore InitializeDocumentStore(Assembly assembly, bool createIndexes)
        {
            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080/",
                DefaultDatabase = "TimeTracking",
                Conventions =
                {
                    DefaultUseOptimisticConcurrency = true,
                    DocumentKeyGenerator = (dbname, commands, entity) => "",
                    SaveEnumsAsIntegers = true,
                    CustomizeJsonSerializer = serializer =>
                    {
                        serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        serializer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                        serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
                        serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                        serializer.NullValueHandling = NullValueHandling.Include;
                        serializer.DefaultValueHandling = DefaultValueHandling.Populate;
                    }
                }
            };

            documentStore.Initialize();

            if (createIndexes)
            {
                IndexCreation.CreateIndexes(assembly, documentStore);
            }

            return documentStore;
        }
    }
}