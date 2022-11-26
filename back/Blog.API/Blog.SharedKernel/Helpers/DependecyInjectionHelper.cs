using Blog.SharedKernel.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.SharedKernel.Helpers
{
    public static class DependecyInjectionHelper
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly assembly)
        {
            foreach (var concreteClass in assembly.DefinedTypes)
            {
                var repositoryInterface = GetInterfaceByName(concreteClass, nameof(IRepository));
                if(repositoryInterface == null)
                {
                    continue;
                }

                services.AddTransient(repositoryInterface, concreteClass);
            }
        }

        private static Type? GetInterfaceByName(TypeInfo concreteClass, string interfaceName)
        {
            return concreteClass
                .GetInterfaces()
                .FirstOrDefault(@interface => @interface.GetInterface(interfaceName) != null);
        }
    }
}
