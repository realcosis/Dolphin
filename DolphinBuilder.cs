using Dolphin.Backgrounds;
using Dolphin.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dolphin
{        
    public class DolphinBuilder(IEmulator emulator) : IHostedService
    {
        public static IHostBuilder CreateDolphinBuilder(string[] args)
            => new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddConfiguration();
                    services.AddSingleton(hostContext.Configuration);
                    services.AddDolphinServices();
                    services.AddHostedService<DolphinBuilder>();
                    services.AddHostedService<Worker>();
                });

        async Task IHostedService.StartAsync(CancellationToken cancellationToken)
            => await emulator.Start();

        async Task IHostedService.StopAsync(CancellationToken cancellationToken)
            => await emulator.Dispose();
    }
}