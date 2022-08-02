using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Config.DIAutoRegister
{
    public static class ServiceCollectionExtension
    {
        public static AutoRegisterData RegisterAssemblyPublicNonGenericClasses(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
                assemblies = new[] { Assembly.GetCallingAssembly() };

            var allPublicTypes = assemblies.SelectMany(x => x.GetExportedTypes()
                .Where(y => y.IsClass && !y.IsAbstract && !y.IsGenericType && !y.IsNested));
            return new AutoRegisterData(services, allPublicTypes);
        }

        public static AutoRegisterData Where(this AutoRegisterData autoRegData, Func<Type, bool> predicate)
        {
            if (autoRegData == null) throw new ArgumentNullException(nameof(autoRegData));
            autoRegData.TypeFilter = predicate;
            return new AutoRegisterData(autoRegData.Services, autoRegData.TypesToConsider.Where(predicate));
        }

        public static IServiceCollection AsPublicImplementedInterfaces(this AutoRegisterData autoRegData,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (autoRegData == null) throw new ArgumentNullException(nameof(autoRegData));
            foreach (var classType in (autoRegData.TypeFilter == null
                ? autoRegData.TypesToConsider
                : autoRegData.TypesToConsider.Where(autoRegData.TypeFilter)))
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces;
                foreach (var infc in interfaces.Where(i => i != typeof(IDisposable) && i.IsPublic && !i.IsNested))
                {
                    autoRegData.Services.Add(new ServiceDescriptor(infc, classType, lifetime));
                }
            }

            return autoRegData.Services;
        }
    }
}
