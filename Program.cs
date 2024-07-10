using Dolphin;
using Dolphin.Configurations;
using Dolphin.ConsoleCommands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RC.Common.Configurations;

var host = new HostBuilder()
.ConfigureServices((builder, services) =>
{
    services.AddConfiguration<Configuration>(ConfigurationTypes.File);
    services.AddSingleton(builder.Configuration);
    services.AddDolphinServices();
}).Build();

var emulator = host.Services.GetRequiredService<IEmulator>();
await emulator.Start();
await host.StartAsync();

while (true)
{
    var input = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(input))
        await input.InvokeCommand(host.Services);
}