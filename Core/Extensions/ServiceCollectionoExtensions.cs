using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionoExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, params ICoreModule[] coreModules)
        {
            foreach (var coreModule in coreModules)
            {
                coreModule.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}
