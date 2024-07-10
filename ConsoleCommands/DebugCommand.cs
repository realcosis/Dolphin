using Microsoft.Extensions.Logging;

namespace Dolphin.ConsoleCommands
{
    class DebugCommand(ILogger<DebugCommand> logger) : ConsoleCommand
    {
        protected override async Task Handle()
        {
            await Task.Run(() =>
            {
                Emulator.Debug = !Emulator.Debug;
                logger.LogInformation("Dolphin is now {status}", Emulator.Debug ? "debugging" : "releasing");
            });
        }
    }
}