using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dolphin.Configurations
{
    public static class ConfigurationExtensions
    {
        public static void AddConfiguration(this IServiceCollection services)
        {
            using var jsonConfiguration = Environment.GetEnvironmentVariable("configurationPath")!.BuildFromFile();

            if (jsonConfiguration != Stream.Null)
            {
                var configuration = new ConfigurationBuilder().AddJsonStream(jsonConfiguration).Build();
                services.Configure<Configuration>(configuration);
            }
        }

        private static MemoryStream BuildFromFile(this string path)
        {
            var result = File.ReadAllBytes(path);
            return new MemoryStream(result);
        }
    }
}