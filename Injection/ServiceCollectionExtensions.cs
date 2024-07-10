using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dolphin.Injection
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterService(this IServiceCollection services, Type type, Assembly assembly)
        {
            if (type.GetCustomAttribute<ScopedAttribute>() != default)
                services.Scan(s => s.FromAssemblies(assembly)
                                .AddClasses(c => c.Where(t => t.IsAssignableTo(type) && !t.IsAbstract && !t.IsInterface))
                                .AsSelfWithInterfaces()
                                .WithScopedLifetime());

            if (type.GetCustomAttribute<SingletonAttribute>() != default)
                services.Scan(s => s.FromAssemblies(assembly)
                                .AddClasses(c => c.Where(t => t.IsAssignableTo(type) && !t.IsAbstract && !t.IsInterface))
                                .AsSelfWithInterfaces()
                                .WithSingletonLifetime());

            if (type.GetCustomAttribute<TransientAttribute>() != default)
                services.Scan(s => s.FromAssemblies(assembly)
                                .AddClasses(c => c.Where(t => t.IsAssignableTo(type) && !t.IsAbstract && !t.IsInterface))
                                .AsSelfWithInterfaces()
                                .WithTransientLifetime());
        }
    }
}