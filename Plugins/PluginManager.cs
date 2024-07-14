using Dolphin.Injection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Dolphin.Plugins
{
    [Scoped]
    class PluginManager(ILogger<IPluginManager> logger, IServiceProvider serviceProvider) : IPluginManager, IStartableService
    {
        HashSet<IPlugin> IPluginManager.Plugins { get; } = [];

        async Task IStartableService.Start()
        {
            if (!Directory.Exists("plugins"))
                Directory.CreateDirectory("plugins");

            var plugins = Directory.GetFiles("plugins", "*.dll");

            foreach (var plugin in plugins)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), plugin));

                    if (assembly == null) return;

                    var pluginTypes = assembly
                                        .GetTypes()
                                        .Where(t => typeof(IPlugin).IsAssignableFrom(t))
                                        .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic && !t.IsGenericType)
                                        .ToList();

                    if (pluginTypes.Count != 0)
                    {
                        foreach (var pluginType in pluginTypes)
                        {
                            CreatePluginInstance(pluginType);
                        }
                    }
                    else
                    {
                        logger.LogError("PluginManager cannot start {plugin} because it doesn't implement IPlugin", plugin);
                    }
                }

                catch (Exception ex)
                {
                    logger.LogError("PluginManager cannot start {plugin} because {ex}", plugin, ex);
                }
            }

            logger.LogInformation("PluginManager has been loaded with {count} plugins definitions", ((IPluginManager)this).Plugins.Count);
        }

        private void CreatePluginInstance(Type pluginType)
        {
            var constructors = pluginType.GetConstructors();
            var firstConstrutor = constructors.FirstOrDefault();

            if (firstConstrutor == default)
                return;

            var parameters = new List<object>();

            foreach (var param in firstConstrutor.GetParameters())
            {
                var service = serviceProvider.GetService(param.ParameterType);
                if (service != default)
                    parameters.Add(service);
            }

            try
            {
                var pluginInstance = Activator.CreateInstance(pluginType, [.. parameters]) as IPlugin;

                if (pluginInstance != default)
                {
                    ((IPluginManager)this).Plugins.Add(pluginInstance);
                    logger.LogInformation("{pluginName} by {pluginAuthor} has been loaded", pluginInstance.Name, pluginInstance.Author);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Exception during activation of a plugin: {ex}", ex);
            }
        }
    }
}