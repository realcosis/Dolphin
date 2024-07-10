using Microsoft.Extensions.Hosting;

namespace Dolphin.ConsoleCommands
{
    class ShutdownCommand(IEmulator emulator, IHost host) : ConsoleCommand
    {
        protected override async Task Handle()
        {
            Console.Clear();
            await host.StopAsync();
            await emulator.Dispose();
        }
    }
}