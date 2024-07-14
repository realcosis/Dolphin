using Dolphin;
using Dolphin.ConsoleCommands;

var host = DolphinBuilder.CreateDolphinBuilder(args).Build();
await host.StartAsync();

while (true)
{
    var input = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(input))
        await input.InvokeCommand(host.Services);
}